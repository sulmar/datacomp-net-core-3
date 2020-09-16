using Bogus;
using DataComp.Training.Models;

namespace DataComp.Training.Fakers
{
    public class ProductFaker : Faker<Product>
    {
        public ProductFaker()
        {
            RuleFor(p => p.Id, f => f.Random.Guid());
        }
    }
}
