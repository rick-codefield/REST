namespace Rest.Application.Repositories;

public interface IUnitOfWork : IAsyncDisposable
{
    IBrandRepository BrandRepository { get; }
    ICompanyRepository CompanyRepository { get; }
    IProductRepository ProductRepository { get; }

    Task Commit(CancellationToken cancellationToken = default);
}
