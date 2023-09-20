using JobNetworkAPI.Application.Repositories;
using JobNetworkAPI.Application.RequestParameters;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Application.Features.Queries.JobPost.GetAllJobPost
{
    public class GetAllJobPostQueryHandler : IRequestHandler<GetAllJobPostQueryRequest, GetAllJobPostQueryResponse>
    {
        readonly IJobPostsReadRepository _jobPostsReadRepository;
        readonly ILogger<GetAllJobPostQueryHandler> _logger;

        public GetAllJobPostQueryHandler(IJobPostsReadRepository jobPostsReadRepository, ILogger<GetAllJobPostQueryHandler> logger)
        {
            _jobPostsReadRepository = jobPostsReadRepository;
            _logger = logger;
        }
        public async Task<GetAllJobPostQueryResponse> Handle(GetAllJobPostQueryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all jobposts");
            var totalCount = _jobPostsReadRepository.GetAll(false).Count();

            var jobPosts = _jobPostsReadRepository.GetAll().Skip(request.Page * request.Size).Take(request.Size).Select(j => new
            {
                j.Id,
                j.JobTypeId,
                j.CompanyName,
                j.Description,
                j.StartDate,
                j.EndDate,
                j.Title,
                j.ImagePath
            }).ToList();

            return new()
            {
                JobPosts = jobPosts,
                TotalCount = totalCount
            };
        }
    }
}
