using JobNetworkAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Domain.Entities
{
    public class JobPostImageFile : File
    {

        public bool Showcase { get; set; }
        public ICollection<JobPosts> JobPost { get; set; }
    }
}
