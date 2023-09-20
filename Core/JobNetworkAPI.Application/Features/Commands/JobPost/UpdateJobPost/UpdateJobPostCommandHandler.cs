using JobNetworkAPI.Application.Repositories;
using JobNetworkAPI.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
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
        readonly ILogger<UpdateJobPostCommandHandler> _logger;

        public UpdateJobPostCommandHandler(IJobPostsReadRepository jobPostsReadRepository, IJobPostsWriteRepository jobPostsWriteRepository, ILogger<UpdateJobPostCommandHandler> logger)
        {
            _jobPostsReadRepository = jobPostsReadRepository;
            _jobPostsWriteRepository = jobPostsWriteRepository;
            _logger = logger;
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

            _logger.LogInformation("İlan güncellendi.");
            return new();

        }
    }
}
