using Microsoft.EntityFrameworkCore;
using TaskPRO.Domain.entities;
using System.Threading;
using System.Threading.Tasks;
using TaskPRO.Domain.enums;

namespace TaskPRO.Application.features.Users.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}