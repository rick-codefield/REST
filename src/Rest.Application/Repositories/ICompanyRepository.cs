using Rest.Application.Entities;

namespace Rest.Application.Repositories;

public interface ICompanyRepository : ISpecificationRepository<Company, CompanyExpansion>, IPagedRepository<Company, CompanyExpansion>
{
}