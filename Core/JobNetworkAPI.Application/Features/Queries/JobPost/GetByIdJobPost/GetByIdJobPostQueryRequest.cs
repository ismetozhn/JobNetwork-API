using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Application.Features.Queries.JobPost.GetByIdJobPost
{
    public class GetByIdJobPostQueryRequest : IRequest<GetByIdJobPostQueryResponse>
    {

        public int Id { get; set; }
    }
}
