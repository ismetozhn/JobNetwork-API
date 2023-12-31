﻿using JobNetworkAPI.Domain.Entities;
using JobNetworkAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobNetworkAPI.Application.Abstractions.Hubs;

namespace JobNetworkAPI.Application.Features.Commands.JobPost.CreateJobPost
{
    public class CreateJobPostCommandHandler : IRequestHandler<CreateJobPostCommandRequest, CreateJobPostCommandResponse>
    {
        readonly IJobPostsWriteRepository _jobPostsWriteRepository;
        readonly IJobPostHubService _jobPostHubService;

        public CreateJobPostCommandHandler(IJobPostsWriteRepository jobPostsWriteRepository, IJobPostHubService jobPostHubService)
        {
            _jobPostsWriteRepository = jobPostsWriteRepository;
            _jobPostHubService = jobPostHubService;
        }
        public async Task<CreateJobPostCommandResponse> Handle(CreateJobPostCommandRequest request, CancellationToken cancellationToken)
        {
            await _jobPostsWriteRepository.AddAsync(new JobPosts
            {
                Title = request.Title,
                CompanyName = request.CompanyName,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ImagePath = request.ImagePath,
                JobTypeId = request.JobTypeId


            });

            await _jobPostsWriteRepository.SaveAsync();
             await _jobPostHubService.JobPostAddedMessageAsync($"{request.Title} adlı ilan yayınlanmıştır.");
            return new();

        }
    }
}
