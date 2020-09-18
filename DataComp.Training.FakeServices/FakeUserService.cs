using Bogus;
using DataComp.Training.IServices;
using DataComp.Training.Models;
using DataComp.Training.Models.SearchCriteria;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Security.Claims;
using System.Threading;

namespace DataComp.Training.FakeServices
{


    public class FakeUserService : FakeEntityService<User, UserSearchCriteria>, IUserService, IAuthenticateService
    {
        public FakeUserService(Faker<User> faker, IOptions<FakeEntityServiceOptions> options) : base(faker, options)
        {
        }

        public override void Remove(Guid id)
        {
            User user = Get(id);
            user.IsRemoved = true;
        }

        private IQueryable<User> GetBySearchCriteria(UserSearchCriteria searchCriteria)
        {
            IQueryable<User> query = entities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchCriteria.FullName))
            {
                query = query.Where(u => u.FullName.StartsWith(searchCriteria.FullName, StringComparison.InvariantCultureIgnoreCase));
            }

            if (searchCriteria.IsRemoved.HasValue)
            {
                query = query.Where(u => u.IsRemoved == searchCriteria.IsRemoved.Value);
            }

            return query;
        }

        public override ICollection<User> Get(UserSearchCriteria searchCriteria)
        {
            var query = GetBySearchCriteria(searchCriteria);

            // SQL - Wyrażenia CTE

            return query.ToList();
        }

        public User Get(string pesel)
        {
            return entities.SingleOrDefault(u => u.Pesel == pesel);
        }

        public bool IsExists(string pesel)
        {
            return entities.Any(u => u.Pesel == pesel);
        }

        public bool TryAuthenticate(string username, string hashedPassword, out User user)
        {
            user = entities.SingleOrDefault(u => u.UserName == username && u.HashedPassword == hashedPassword);

            return user != null;
        }

        public ICollection<User> Get(UserSearchCriteria searchCriteria, PermissionCriteria permissionCriteria)
        {
            var query = GetBySearchCriteria(searchCriteria);

            if (permissionCriteria.Departments!=null)
            {
                // query = query.Where()
            }

            throw new NotImplementedException();
        }
    }
}
