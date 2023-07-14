using System.Runtime.CompilerServices;
using Rest.Application.Entities;
using Rest.Application.Repositories;
using Rest.Application.Specifications;

namespace Rest.Infrastructure.Repositories;

internal sealed class InMemoryProductRepository : InMemoryRepository<Product, ProductExpansion>, IProductRepository
{
    public InMemoryProductRepository(InMemoryUnitOfWork unitOfWork):
        base(unitOfWork)
    {
    }

    public override async IAsyncEnumerable<Product> Get(ISpecification<Product> specification, ProductExpansion? expansion, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        foreach (var product in base.GetSync(specification, expansion, cancellationToken))
        {
            if (expansion != null)
            {
                if (expansion.Brand != null)
                {
                    product.Brand = (await UnitOfWork.BrandRepository.GetById(product.Brand.Id, expansion.Brand, cancellationToken))!;
                }
            }

            yield return product;
        }
    }

    public override async Task<PagedResult<Product>> GetPaged(IContinuationToken? continuationToken = null, ProductExpansion? expansion = null, CancellationToken cancellationToken = default)
    {
        var products = await base.GetPaged(continuationToken, expansion, cancellationToken);

        if (expansion != null)
        {
            if (expansion.Brand != null)
            {
                foreach (var product in products.Data)
                {
                    product.Brand = (await UnitOfWork.BrandRepository.GetById(product.Brand.Id, expansion.Brand, cancellationToken))!;
                }
            }
        }

        return products;
    }
}
