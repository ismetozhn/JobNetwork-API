﻿using JobNetworkAPI.Application.RequestParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Application.Features.Queries.JobPost.GetAllJobPost
{
    public class GetAllJobPostQueryRequest : IRequest<GetAllJobPostQueryResponse>
    {

        // public Pagination Pagination { get; set; }  

        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
