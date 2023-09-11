using JobNetworkAPI.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Domain.Entities
{
    public class JobPostImageFile : File
    {
        public ICollection<JobPosts> JobPost { get; set; }
    }
}
