using Microsoft.EntityFrameworkCore;
using UrlShorter.Models;

namespace UrlShorter
{
    public class LinksContext : DbContext
    {

        public DbSet<Link> Links { get; set; }

        public LinksContext(DbContextOptions<LinksContext> options): base(options)
        {
            Database.EnsureCreated();
        }

    }
}
