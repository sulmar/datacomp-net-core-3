using DataComp.Training.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataComp.Training.Api.Events
{
    // dotnet add package MediatR
    public class UserRemovedEvent : INotification   // mark interface (znacznik)
    {
        public User User { get; set; }

        public UserRemovedEvent(User user)
        {
            User = user;
        }
    }
}
