using JobNetworkAPI.Application.Repositories;
using JobNetworkAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Application.Features.Commands.JobPost.UpdateJobPost
{
    public class UpdateJobPostCommandHandler : IRequestHandler<UpdateJobPostCommandRequest, UpdateJobPostCommandResponse>
    {

        readonly IJobPostsReadRepository _jobPostsReadRepository;
        readonly IJobPostsWriteRepository _jobPostsWriteRepository;

        public UpdateJobPostCommandHandler(IJobPostsReadRepository jobPostsReadRepository, IJobPostsWriteRepository jobPostsWriteRepository)
        {
            _jobPostsReadRepository = jobPostsReadRepository;
            _jobPostsWriteRepository = jobPostsWriteRepository;
        }

        public async Task<UpdateJobPostCommandResponse> Handle(UpdateJobPostCommandRequest request, CancellationToken cancellationToken)
        {
            JobPosts jobpost = await _jobPostsReadRepository.GetByIdAsync(request.Id);

            jobpost.Title = request.Title;
            jobpost.Description = request.Description;
            jobpost.StartDate = request.StartDate;
            jobpost.EndDate = request.EndDate;
            jobpost.CompanyName = request.CompanyName;
            jobpost.ImagePath = request.ImagePath;

            await _jobPostsWriteRepository.SaveAsync();
            return new();

        }
    }
}
