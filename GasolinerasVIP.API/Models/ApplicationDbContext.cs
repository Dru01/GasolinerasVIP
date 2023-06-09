using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GasolinerasVIP.API.Models;

namespace GasolinerasVIP.API.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public virtual DbSet<Transaction> Transaction{ get; set; }
        public virtual DbSet<GasStation> GasStation { get; set; }
        public virtual DbSet<UserRefreshToken> UserRefreshToken { get; set; }
    }
}