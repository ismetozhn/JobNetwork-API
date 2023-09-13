using JobNetworkAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;

namespace JobNetworkAPI.Domain.Entities;

public partial class JobApplications : BaseEntity
{
    

    public int JobPostId { get; set; }

    public int UserId { get; set; }

    public int ApplyStatusId { get; set; }

    public DateTime ApplyDate { get; set; }



  
    public virtual ApplyStatus ApplyStatus { get; set; } = null!;

    public virtual JobPosts JobPost { get; set; } = null!;

    public virtual Users User { get; set; } = null!;
}
