using DataModels;

using Microsoft.EntityFrameworkCore;

namespace Repositories
{

    public class myappcontext : DbContext
    {

        public myappcontext(DbContextOptions options) : base(options) { }
        
        // public DbSet<classification>
        public DbSet<Ipobj> Iplists { get; set; }
     //   public DbSet<entities> Entities { get; set; }
        public DbSet<classification> classifications { get; set; }
        public DbSet<commentsobj> comments { get; set; }
        public DbSet<countries> countries { get; set; }

        public DbSet<Currency> Currency { get; set; }
        public DbSet<reportstatus> reportstatus { get; set; }


    }
}
