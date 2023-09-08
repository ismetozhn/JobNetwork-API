﻿using JobNetworkAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Application.Repositories
{
    public interface IJobPostImageFileWriteRepository: IWriteRepository<JobPostImageFile>
    {
    }
}
