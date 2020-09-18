using DataComp.Training.Models;
using DataComp.Training.Models.SearchCriteria;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DataComp.Training.IServices
{

    public interface IUserService : IEntityService<User, UserSearchCriteria>
    {
        User Get(string pesel);
        bool IsExists(string pesel);
        ICollection<User> Get(UserSearchCriteria searchCriteria, PermissionCriteria permissionCriteria);
    }
}
