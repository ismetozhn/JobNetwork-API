using JobNetworkAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;

namespace JobNetworkAPI.API;

public partial class JobTypes : BaseEntity
{
 

    public string Type { get; set; } = null!;

   

   

    public virtual ICollection<JobPosts> JobPosts { get; set; } = new List<JobPosts>();
}
