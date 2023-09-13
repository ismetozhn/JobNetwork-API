using JobNetworkAPI.Application.Repositories;
using JobNetworkAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Application.Features.Queries.JobPostImageFile.GetJobPostImages
{
    public class GetProductImagesQueryHandler : IRequestHandler<GetJobPostImagesQueryRequest, List<GetJobPostImagesQueryResponse>>
    {

        readonly IJobPostsReadRepository _jobPostsReadRepository;

        readonly IConfiguration configuration;

        public GetProductImagesQueryHandler(IJobPostsReadRepository jobPostsReadRepository, IConfiguration configuration)
        {
            _jobPostsReadRepository = jobPostsReadRepository;
            this.configuration = configuration;
        }

        public async Task<List<GetJobPostImagesQueryResponse>> Handle(GetJobPostImagesQueryRequest request, CancellationToken cancellationToken)
        {
            JobPosts? jobpost = await _jobPostsReadRepository.Table.Include(p => p.JobPostImageFiles).FirstOrDefaultAsync(p => p.Id == request.Id);

            //await Task.Delay(2000);

            return jobpost?.JobPostImageFiles.Select(p => new GetJobPostImagesQueryResponse
            {
                Path = $"{configuration["BaseStorageUrl"]}/{p.Path}",
                FileName = p.FileName,
                Id = p.Id
            }).ToList();
        }
    }
}
