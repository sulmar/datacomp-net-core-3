using DataComp.Training.Models;
using MediatR;
using System;

namespace DataComp.Training.Api.Requests
{
    public class GetUserRequest : IRequest<User>
    {
        public GetUserRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }


    }
}
