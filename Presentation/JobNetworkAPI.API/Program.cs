using FluentValidation.AspNetCore;
using JobAdvertAPI.Infrastructure.Filters;
using JobNetworkAPI.Application.Validators.JobPosts;
using JobNetworkAPI.Infrastructure;
using JobNetworkAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();




builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateJobPostValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy=>
    //policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
   policy.WithOrigins("https://localhost:4200/", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod()
));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();