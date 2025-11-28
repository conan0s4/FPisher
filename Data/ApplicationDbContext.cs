using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FPisher.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FPisher.Models.Login_records> Login_Records { get; set; }
        public DbSet<FPisher.Models.Campaign_records> Campaign_Records { get; set; }
        public DbSet<FPisher.Models.PageVisits> Page_Visits { get; set; }

    }
}
