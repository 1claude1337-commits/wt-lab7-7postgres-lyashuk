using Microsoft.EntityFrameworkCore;
using wt_lab7_7postgres_efcore_lyashuk.Models;

namespace wt_lab7_7postgres_efcore_lyashuk.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Player> Players => Set<Player>();
}