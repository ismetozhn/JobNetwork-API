using JobNetworkAPI.Application.Abstractions.Storage;
using JobNetworkAPI.Application.Repositories;
using JobNetworkAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Application.Features.Commands.JobPostImageFile.UploadJobPostImage
{
    public class UploadJobPostImageCommandHandler : IRequestHandler<UploadJobPostImageCommandRequest, UploadJobPostImageCommandResponse>
    {

        readonly IJobPostsReadRepository _jobPostsReadRepository;
        readonly IStorageService _storageService;
        readonly IJobPostImageFileWriteRepository _jobPostImageFileWriteRepository;

        public UploadJobPostImageCommandHandler(IJobPostsReadRepository jobPostsReadRepository, IStorageService storageService, IJobPostImageFileWriteRepository jobPostImageFileWriteRepository)
        {
            _jobPostsReadRepository = jobPostsReadRepository;
            _storageService = storageService;
            _jobPostImageFileWriteRepository = jobPostImageFileWriteRepository;
        }

        public async Task<UploadJobPostImageCommandResponse> Handle(UploadJobPostImageCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("files", request.Files);

            JobPosts jobpost = await _jobPostsReadRepository.GetByIdAsync(request.Id);

            await _jobPostImageFileWriteRepository.AddRangeAsync(result.Select(r => new Domain.Entities.JobPostImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageService.StorageName,
                JobPost = new List<JobPosts> { jobpost }

            }).ToList());

            await _jobPostImageFileWriteRepository.SaveAsync();

            return new();

        }
    }
}
