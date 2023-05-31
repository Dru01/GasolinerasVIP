using GasolinerasVIP.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GuiaDCEA.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Test> registroTest { get; set; }
    }
}