using DataComp.Training.Models;
using DataComp.Training.Models.SearchCriteria;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataComp.Training.IServices
{
    public interface IProductService : IEntityService<Product, ProductSearchCriteria>
    {
        ICollection<Product> GetByCustomer(Guid customerId);
    }


    public interface IProductServiceAsync : IEntityServiceAsync<Product, ProductSearchCriteria>
    {
        Task<ICollection<Product>> GetByCustomerAsync(Guid customerId);
    }
}
