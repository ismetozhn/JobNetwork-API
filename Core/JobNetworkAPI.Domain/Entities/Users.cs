using JobNetworkAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;

namespace JobNetworkAPI.API;

public partial class Users : BaseEntity
{
    

    public int UserTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? ContactNumber { get; set; }

    public string? Cv { get; set; }

 

    

    public virtual ICollection<JobApplications> JobApplications { get; set; } = new List<JobApplications>();

    public virtual UserTypes UserType { get; set; } = null!;
}
