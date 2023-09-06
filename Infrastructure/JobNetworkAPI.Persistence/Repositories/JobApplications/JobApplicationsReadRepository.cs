
using JobNetworkAPI.API;
using JobNetworkAPI.Application.Repositories;
using JobNetworkAPI.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Persistence.Repositories
{
    public class JobApplicationsReadRepository : ReadRepository<JobApplications>, IJobApplicationsReadRepository
    {
        public JobApplicationsReadRepository(JobNetworkkDbContext context) : base(context)
        {
        }
    }
}
