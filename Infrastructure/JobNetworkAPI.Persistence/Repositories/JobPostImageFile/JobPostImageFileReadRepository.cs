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
    public class JobPostImageFileReadRepository : ReadRepository<JobPostImageFile>, IJobPostImageFileReadRepository
    {
        public JobPostImageFileReadRepository(JobNetworkkDbContext context) : base(context)
        {
        }
    }
}
