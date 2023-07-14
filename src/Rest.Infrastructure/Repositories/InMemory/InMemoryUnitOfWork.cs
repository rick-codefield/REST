using Rest.Application.Repositories;

namespace Rest.Infrastructure.Repositories;

public sealed class InMemoryUnitOfWork : IUnitOfWork
{
    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }

    public IBrandRepository BrandRepository => _brandRepository ??= new(this);
    public ICompanyRepository CompanyRepository => _companyRepository ??= new(this);
    public IProductRepository ProductRepository => _productRepository ??= new(this);

    public Task Commit(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    private InMemoryBrandRepository? _brandRepository;
    private InMemoryCompanyRepository? _companyRepository;
    private InMemoryProductRepository? _productRepository;
}
