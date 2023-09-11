using JobNetworkAPI.Domain.Entities;
using JobNetworkAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;

namespace JobNetworkAPI.API;

public partial class JobPosts : BaseEntity
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

    public ICollection<JobPostImageFile> JobPostImageFiles { get; set; }
}
