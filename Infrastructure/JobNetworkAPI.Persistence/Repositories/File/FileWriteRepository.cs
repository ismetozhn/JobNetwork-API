using JobNetworkAPI.API;
using JobNetworkAPI.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Persistence.Repositories
{
    public class FileWriteRepository : WriteRepository<JobNetworkAPI.Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(JobNetworkkDbContext context) : base(context)
        {
        }
    }
}
