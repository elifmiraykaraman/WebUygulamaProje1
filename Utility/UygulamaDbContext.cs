using Microsoft.EntityFrameworkCore;

namespace WebUygulamaProje1.Utility
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }

        public DbSet<WebUygulamaProje1.Models.KitapTuru> KitapTurleri { get; set; }

    }
}
