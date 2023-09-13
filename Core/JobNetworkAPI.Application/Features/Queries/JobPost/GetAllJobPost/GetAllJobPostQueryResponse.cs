using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Application.Features.Queries.JobPost.GetAllJobPost
{
    public class GetAllJobPostQueryResponse
    {
        public int TotalCount { get; set; }
        public object JobPosts { get; set; }
    }
}
