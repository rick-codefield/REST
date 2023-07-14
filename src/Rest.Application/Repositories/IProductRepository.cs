using Rest.Application.Entities;

namespace Rest.Application.Repositories;

public interface IProductRepository : ISpecificationRepository<Product, ProductExpansion>, IPagedRepository<Product, ProductExpansion>
{
}
