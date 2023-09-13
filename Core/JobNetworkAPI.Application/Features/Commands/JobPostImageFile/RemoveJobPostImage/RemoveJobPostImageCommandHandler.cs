using JobNetworkAPI.Application.Repositories;
using JobNetworkAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace JobNetworkAPI.Application.Features.Commands.JobPostImageFile.RemoveJobPostImage
{
    public class RemoveJobPostImageCommandHandler : IRequestHandler<RemoveJobPostImageCommandRequest, RemoveJobPostImageCommandResponse>
    {

        readonly IJobPostsReadRepository _jobPostsReadRepository;
        readonly IJobPostsWriteRepository _jobPostsWriteRepository;

        public RemoveJobPostImageCommandHandler(IJobPostsReadRepository jobPostsReadRepository, IJobPostsWriteRepository jobPostsWriteRepository)
        {
            _jobPostsReadRepository = jobPostsReadRepository;
            _jobPostsWriteRepository = jobPostsWriteRepository;
        }

        public async Task<RemoveJobPostImageCommandResponse> Handle(RemoveJobPostImageCommandRequest request, CancellationToken cancellationToken)
        {
            JobPosts? jobpost = await _jobPostsReadRepository.Table.Include(p => p.JobPostImageFiles).FirstOrDefaultAsync(p => p.Id == int.Parse(request.Id));

            Domain.Entities.JobPostImageFile? jobpostImageFile = jobpost?.JobPostImageFiles.FirstOrDefault(p => p.Id == int.Parse(request.ImageId));



            if (jobpostImageFile != null)
            jobpost?.JobPostImageFiles.Remove(jobpostImageFile);

            await _jobPostsWriteRepository.SaveAsync();

            return new();
        }
    }
}
