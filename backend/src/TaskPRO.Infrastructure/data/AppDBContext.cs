using Microsoft.EntityFrameworkCore;

namespace TaskPRO.Infrastructure.Data;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }
}