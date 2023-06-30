using OnlineStoreApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using OnlineStoreApi.Domain.Entities;

namespace OnlineStoreApi.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Color> Colors { get; }
    DbSet<Size> Sizes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
