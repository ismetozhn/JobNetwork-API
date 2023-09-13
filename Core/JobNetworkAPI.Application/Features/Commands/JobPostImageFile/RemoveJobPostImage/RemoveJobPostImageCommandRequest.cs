using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Application.Features.Commands.JobPostImageFile.RemoveJobPostImage
{
    public class RemoveJobPostImageCommandRequest : IRequest<RemoveJobPostImageCommandResponse>
    {
        public string Id { get; set; }
        public string? ImageId { get; set; }

    }
}
