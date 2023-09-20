using JobNetworkAPI.Application.Abstractions.Hubs;
using JobNetworkAPI.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection collection)
        {
            collection.AddTransient<IJobPostHubService, JobPostHubService>();
            collection.AddSignalR();
        }
    }
}
