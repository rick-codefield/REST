using Rest.Application.Entities;

namespace Rest.Application.Repositories;

public interface IBrandRepository : ISpecificationRepository<Brand, BrandExpansion>, IPagedRepository<Brand, BrandExpansion>
{
}
