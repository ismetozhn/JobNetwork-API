using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Application.Abstractions.Hubs
{
    public interface IJobPostHubService
    {
        Task JobPostAddedMessageAsync(string message);
    }
}
