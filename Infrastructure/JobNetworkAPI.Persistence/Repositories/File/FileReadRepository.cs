using JobNetworkAPI.API;
using JobNetworkAPI.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Persistence.Repositories
{
    public class FileReadRepository : ReadRepository<JobNetworkAPI.Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(JobNetworkkDbContext context) : base(context)
        {
        }
    }
}
