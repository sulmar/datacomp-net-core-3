using Bogus;
using DataComp.Training.IServices;
using DataComp.Training.Models;
using DataComp.Training.Models.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataComp.Training.FakeServices
{
    public class FakeProductServiceAsync : FakeEntityServiceAsync<Product, ProductSearchCriteria>, IProductServiceAsync
    {
        public FakeProductServiceAsync(Faker<Product> faker) : base(faker)
        {
        }

        public Task<ICollection<Product>> GetByCustomerAsync(Guid customerId)
        {
            throw new NotImplementedException();
        }
    }

}
