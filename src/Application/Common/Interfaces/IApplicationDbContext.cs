using Golobal_IMC_Task.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categorys { get; set; }

        DbSet<Product> Products { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
