using Bogus;
using DataComp.Training.IServices;
using DataComp.Training.Models;
using DataComp.Training.Models.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;

namespace DataComp.Training.FakeServices
{


    public class FakeUserService : FakeEntityService<User, UserSearchCriteria>, IUserService
    {
        public FakeUserService(Faker<User> faker) : base(faker)
        {
        }

        public override void Remove(Guid id)
        {
            User user = Get(id);
            user.IsRemoved = true;
        }

        public override ICollection<User> Get(UserSearchCriteria searchCriteria)
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

            // SQL - Wyrażenia CTE

            return query.ToList();
        }

        public User Get(string pesel)
        {
            return entities.SingleOrDefault(u => u.Pesel == pesel);
        }
    }
}
