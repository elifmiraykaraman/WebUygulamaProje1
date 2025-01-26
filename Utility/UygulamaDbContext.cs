using Microsoft.EntityFrameworkCore;
using WebUygulamaProje1.Models;

//Veri tabanında EF Tablos oluşturması için ilgili model yapılarını buraya ekleriz.
namespace WebUygulamaProje1.Utility
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }

        public DbSet<KitapTuru> KitapTurleri { get; set; }
        public DbSet<Kitap> Kitaplar { get; set; }

        public DbSet<Kiralama> Kiralamalar { get; set; }

    }
}
