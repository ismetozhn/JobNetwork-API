

using JobNetworkAPI.API;
using JobNetworkAPI.Application.Repositories;


using JobNetworkAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<JobNetworkkDbContext>(options => options.UseSqlServer("Server=ismetozhn\\SQLEXPRESS;Database=JobNetworkDb;User Id=sa;Password=1234; TrustServerCertificate=True"));

            services.AddScoped<IUsersReadRepository,UsersReadRepository >();
            services.AddScoped<IUsersWriteRepository, UsersWriteRepository>();

            services.AddScoped<IJobPostsReadRepository, JobPostsReadRepository>();
            services.AddScoped<IJobPostsWriteRepository, JobPostsWriteRepository>();

            services.AddScoped<IJobApplicationsReadRepository, JobApplicationsReadRepository>();
            services.AddScoped<IJobApplicationsWriteRepository, JobApplicationsWriteRepository>();

            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();

            services.AddScoped<IJobPostImageFileReadRepository, JobPostImageFileReadRepository>();
            services.AddScoped<IJobPostImageFileWriteRepository, JobPostImageFileWriteRepository>();

            services.AddScoped<ICvFileReadRepository, CvFileReadRepository>();
            services.AddScoped<ICvFileWriteRepository, CvFileWriteRepository>();





        }
    }
}
