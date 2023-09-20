using FluentValidation.AspNetCore;
using JobNetworkAPI.Infrastructure.Filters;
using JobNetworkAPI.Application.Validators.JobPosts;
using JobNetworkAPI.Infrastructure;
using JobNetworkAPI.Persistence;
using JobNetworkAPI.Infrastructure.Services.Storage.Local;
using JobNetworkAPI.Infrastructure.Services.Storage.Azure;
using JobNetworkAPI.Application;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Security.Claims;
using Serilog.Context;
using JobNetworkAPI.API.Configurations.ColumnWriters;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.HttpLogging;
using JobNetworkAPI.API.Extensions;
using JobNetworkAPI.SignalR;
using JobNetworkAPI.SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddSignalRServices();

//builder.Services.AddStorage<LocalStorage>();
builder.Services.AddStorage<AzureStorage>();
builder.Services.AddHttpClient();




builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateJobPostValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    //policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
   policy.WithOrigins("https://localhost:4200/", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));

SqlColumn sqlColumn = new SqlColumn();
sqlColumn.ColumnName = "Email";
sqlColumn.DataType = System.Data.SqlDbType.NVarChar;
sqlColumn.PropertyName = "Email";
sqlColumn.DataLength = 50;
sqlColumn.AllowNull = true;
ColumnOptions columnOpt = new ColumnOptions();
columnOpt.Store.Remove(StandardColumn.Properties);
columnOpt.Store.Add(StandardColumn.LogEvent);
columnOpt.AdditionalColumns = new Collection<SqlColumn> { sqlColumn };

Logger log = new LoggerConfiguration()
.WriteTo.Console()
.WriteTo.File("logs/log.txt")
.WriteTo.MSSqlServer(
connectionString: builder.Configuration.GetConnectionString("MsSQL"),
sinkOptions: new MSSqlServerSinkOptions
{
    AutoCreateSqlTable = true,
    TableName = "Logs",
},
appConfiguration: null,
columnOptions: columnOpt
)

.Enrich.FromLogContext()
.Enrich.With<CustomUserNameColumn>()
.MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);

var email = "user@example.com";
log.Information("Bu bir bilgi logu. Email: {Email}", email);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;

});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

            NameClaimType = ClaimTypes.Name


        };
    });

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout=TimeSpan.FromMinutes(10);
});  

var app = builder.Build();

app.UseSession();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseStaticFiles();
app.UseSerilogRequestLogging();

app.UseHttpLogging();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("UserName", username);
    await next();
});

app.MapControllers();
app.MapHubs();

app.Run();
