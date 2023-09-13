using JobNetworkAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;

namespace JobNetworkAPI.Domain.Entities;

public partial class ApplyStatus : BaseEntity
{
   

    public string ApplyStatus1 { get; set; } = null!;



    public virtual ICollection<JobApplications> JobApplications { get; set; } = new List<JobApplications>();
}
