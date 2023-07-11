using Rest.Application.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Rest.Application.Repositories;

public interface IReadRepository<TEntity> : IRepository<TEntity>
    where TEntity : Entity<TEntity>
{
    public Task<TEntity> GetById(Id<TEntity> id, CancellationToken cancellationToken = default);
}
