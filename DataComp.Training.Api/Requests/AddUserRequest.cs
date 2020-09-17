using DataComp.Training.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataComp.Training.Api.Requests
{

    public class AddUserRequest : IRequest<User>
    {
        public AddUserRequest(User user)
        {
            User = user;
        }

        public User User { get; set; }
    }
}
