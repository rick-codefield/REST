using Rest.Application.Entities;
using Rest.Application.Repositories;

namespace Rest.Infrastructure.Repositories;

internal sealed class InMemoryProductRepository : InMemoryRepository<Product>, IProductRepository
{
}
