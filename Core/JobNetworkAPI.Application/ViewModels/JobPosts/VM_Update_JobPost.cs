﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Application.ViewModels.JobPosts
{
    public class VM_Update_JobPost
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ImagePath { get; set; }
    }
}
