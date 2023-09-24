﻿using MediatR;


namespace JobNetworkAPI.Application.Features.Queries.Role.GetRoleById
{
    public class GetRoleByIdQueryRequest : IRequest <GetRoleByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
