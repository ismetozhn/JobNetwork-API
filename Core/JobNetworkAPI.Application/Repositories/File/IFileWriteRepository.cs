using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Application.Repositories
{
    public interface IFileWriteRepository : IWriteRepository<JobNetworkAPI.Domain.Entities.File>
    {
    }
}
