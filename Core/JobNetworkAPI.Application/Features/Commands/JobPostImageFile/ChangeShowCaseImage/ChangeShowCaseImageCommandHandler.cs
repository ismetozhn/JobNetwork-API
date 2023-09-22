using JobNetworkAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Application.Features.Commands.JobPostImageFile.ChangeShowCaseImage
{
    public class ChangeShowCaseImageCommandHandler : IRequestHandler<ChangeShowCaseImageCommandRequest, ChangeShowCaseImageCommandResponse>
    {

        readonly IJobPostImageFileWriteRepository _jobPostImageFileWriteRepository;

        public ChangeShowCaseImageCommandHandler(IJobPostImageFileWriteRepository jobPostImageFileReadRepository)
        {
            _jobPostImageFileWriteRepository = jobPostImageFileReadRepository;
        }

        public async Task<ChangeShowCaseImageCommandResponse> Handle(ChangeShowCaseImageCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _jobPostImageFileWriteRepository.Table
                      .Include(p => p.JobPost)
                      .SelectMany(p => p.JobPost, (pif, p) => new
                      {
                          pif,
                          p
                      });

            var data = await query.FirstOrDefaultAsync(p => p.p.Id == int.Parse(request.JobPostId) && p.pif.Showcase);

            if (data != null)
                data.pif.Showcase = false;

            var image = await query.FirstOrDefaultAsync(p => p.pif.Id == int.Parse(request.ImageId));
            if (image != null)
                image.pif.Showcase = true;

            await _jobPostImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}