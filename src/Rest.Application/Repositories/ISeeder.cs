namespace Rest.Application.Repositories;

public interface ISeeder
{
    public Task Seed(IUnitOfWork unitOfWork, CancellationToken cancellationToken = default);
}
