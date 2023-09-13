using JobNetworkAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Application.Features.Queries.JobPost.GetByIdJobPost
{
    public class GetByIdJobPostQueryResponse
    {
        public int UserId { get; set; }

        public int JobTypeId { get; set; }

        public string Title { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ImagePath { get; set; } = null!;





        public virtual ICollection<JobApplications> JobApplications { get; set; } = new List<JobApplications>();

        public virtual JobTypes JobType { get; set; } = null!;

     
    }
}
