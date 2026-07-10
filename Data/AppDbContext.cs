using Microsoft.EntityFrameworkCore;
using ProfitCalcApp.Models;   // ProfitRecord を使うため

namespace ProfitCalcApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ProfitRecord> ProfitRecords { get; set; }
    }
}
