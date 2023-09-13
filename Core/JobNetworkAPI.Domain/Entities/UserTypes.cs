using JobNetworkAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;

namespace JobNetworkAPI.Domain.Entities;

public partial class UserTypes : BaseEntity
{
    

    public string UserType1 { get; set; } = null!;

  

    

    public virtual ICollection<Users> Users { get; set; } = new List<Users>();
}
