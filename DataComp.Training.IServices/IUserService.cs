using DataComp.Training.Models;
using DataComp.Training.Models.SearchCriteria;
using System;
using System.Collections;

namespace DataComp.Training.IServices
{

    public interface IUserService : IEntityService<User, UserSearchCriteria>
    {
        User Get(string pesel);
    }
}
