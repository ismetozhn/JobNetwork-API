using JobNetworkAPI.Application.Abstractions.Hubs;
using JobNetworkAPI.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JobNetworkAPI.SignalR.HubServices
{
    public class JobPostHubService : IJobPostHubService
    {

        readonly IHubContext<JobPostHub> _hubContext;

        public JobPostHubService(IHubContext<JobPostHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task JobPostAddedMessageAsync(string message)
        {
           await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.JobPostAddedMessage, message);
        }
    }
}
