using Rest.Application.Entities;
using Rest.Application.Repositories;

namespace Rest.Infrastructure.Repositories;

internal sealed class InMemoryCompanyRepository : InMemoryRepository<Company, CompanyExpansion>, ICompanyRepository
{
    public InMemoryCompanyRepository(InMemoryUnitOfWork unitOfWork):
        base(unitOfWork)
    {
    }
}
