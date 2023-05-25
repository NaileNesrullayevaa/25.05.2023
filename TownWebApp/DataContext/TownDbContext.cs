using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TownWebApp.Models;

namespace TownWebApp.DataContext
{
    public class TownDbContext:IdentityDbContext<AppUser>
    {
        public TownDbContext(DbContextOptions<TownDbContext> options): base(options)
        {

        }
        public DbSet<Introduction> Introductions { get; set; }
    }
}
