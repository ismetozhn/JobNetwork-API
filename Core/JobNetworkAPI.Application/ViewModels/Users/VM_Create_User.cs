using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Application.ViewModels.Users
{
    public class VM_Create_User
    {
        public int UserTypeId { get; set; }

        public string Name { get; set; } 

        public string Surname { get; set; } 

        public string Email { get; set; }

        public string Password { get; set; }

        public string? ContactNumber { get; set; }

        public string? Cv { get; set; }
    }
}
