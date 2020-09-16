using Bogus;
using DataComp.Training.IServices;
using DataComp.Training.Models;
using DataComp.Training.Models.SearchCriteria;
using System;
using System.Collections.Generic;

namespace DataComp.Training.FakeServices
{
    public class FakeProductService : FakeEntityService<Product, ProductSearchCriteria>, IProductService
    {
        public FakeProductService(Faker<Product> faker) : base(faker)
        {
        }

        public override ICollection<Product> Get(ProductSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public ICollection<Product> GetByCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }
    }

}
