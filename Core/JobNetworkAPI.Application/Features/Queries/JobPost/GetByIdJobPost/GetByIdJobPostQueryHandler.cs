using JobNetworkAPI.Application.Repositories;
using MediatR;
using P = JobNetworkAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Application.Features.Queries.JobPost.GetByIdJobPost
{
    internal class GetByIdJobPostQueryHandler : IRequestHandler<GetByIdJobPostQueryRequest, GetByIdJobPostQueryResponse>
    {

        readonly IJobPostsReadRepository _jobPostsReadRepository;

        public GetByIdJobPostQueryHandler(IJobPostsReadRepository jobPostsReadRepository)
        {
            _jobPostsReadRepository = jobPostsReadRepository;
        }

        public async Task<GetByIdJobPostQueryResponse> Handle(GetByIdJobPostQueryRequest request, CancellationToken cancellationToken)
        {
            P.JobPosts jobpost = await _jobPostsReadRepository.GetByIdAsync(request.Id, false);
            return new() 
            { 
                Title = jobpost.Title,
                CompanyName = jobpost.CompanyName,
                Description = jobpost.Description,
                StartDate = jobpost.StartDate,
                EndDate = jobpost.EndDate,
                ImagePath = jobpost.ImagePath,
                JobTypeId = jobpost.JobTypeId
            };
        }
    }
}
