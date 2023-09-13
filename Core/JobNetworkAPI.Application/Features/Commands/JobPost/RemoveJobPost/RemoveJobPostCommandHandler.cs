using JobNetworkAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Application.Features.Commands.JobPost.RemoveJobPost
{
    public class RemoveJobPostCommandHandler : IRequestHandler<RemoveJobPostCommandRequest, RemoveJobPostCommandResponse>
    {

        readonly IJobPostsWriteRepository _jobPostsWriteRepository;

        public RemoveJobPostCommandHandler(IJobPostsWriteRepository jobPostsWriteRepository)
        {
            _jobPostsWriteRepository = jobPostsWriteRepository;
        }

        public async Task<RemoveJobPostCommandResponse> Handle(RemoveJobPostCommandRequest request, CancellationToken cancellationToken)
        {
            await _jobPostsWriteRepository.RemoveAsync(request.Id);
            await _jobPostsWriteRepository.SaveAsync();
            return new();
        }
    }
}
