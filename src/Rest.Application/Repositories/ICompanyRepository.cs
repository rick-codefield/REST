using Rest.Application.Entities;

namespace Rest.Application.Repositories;

public interface ICompanyRepository : IReadRepository<Product>
{
}