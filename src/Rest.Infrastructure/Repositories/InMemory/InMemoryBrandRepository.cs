using System.Runtime.CompilerServices;
using Rest.Application.Entities;
using Rest.Application.Repositories;
using Rest.Application.Specifications;

namespace Rest.Infrastructure.Repositories;

internal sealed class InMemoryBrandRepository : InMemoryRepository<Brand, BrandExpansion>, IBrandRepository
{
    public InMemoryBrandRepository(InMemoryUnitOfWork unitOfWork):
        base(unitOfWork)
    {
    }

    public override async IAsyncEnumerable<Brand> Get(ISpecification<Brand> specification, BrandExpansion? expansion, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        foreach (var brand in base.GetSync(specification, expansion, cancellationToken))
        {
            if (expansion != null)
            {
                if (expansion.Company != null)
                {
                    brand.Company = (await UnitOfWork.CompanyRepository.GetById(brand.Company.Id, expansion.Company, cancellationToken))!;
                }
            }

            yield return brand;
        }
    }

    public override async Task<PagedResult<Brand>> GetPaged(IContinuationToken? continuationToken = null, BrandExpansion? expansion = null, CancellationToken cancellationToken = default)
    {
        var brands = await base.GetPaged(continuationToken, expansion, cancellationToken);

        if (expansion != null)
        {
            if (expansion.Company != null)
            {
                foreach (var brand in brands.Data)
                {
                    brand.Company = (await UnitOfWork.CompanyRepository.GetById(brand.Company.Id, expansion.Company, cancellationToken))!;
                }
            }
        }

        return brands;
    }
}
