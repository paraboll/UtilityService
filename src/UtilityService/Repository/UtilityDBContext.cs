using Microsoft.EntityFrameworkCore;
using UtilityService.Models;

namespace UtilityService.Repository
{
    public class UtilityDBContext : DbContext
    {
        public DbSet<Calculations> HistoryCalculations { get; set; }
        public DbSet<CounterValues> HistoryCounterValues { get; set; }

        public DbSet<Coefficients> HistoryCoefficients { get; set; }
        public DbSet<CurrentCoefficients> CurrentCoefficients { get; set; }


        public UtilityDBContext(DbContextOptions<UtilityDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrentCoefficients>().ToTable("CurrentCoefficients");
            modelBuilder.Entity<CurrentCoefficients>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Calculations>().ToTable("HistoryCalculations");
            modelBuilder.Entity<Calculations>(entity =>
            {
                entity.HasKey(e => e.Id);


                entity.HasOne(с => с.Coefficients).
                       WithOne(с => с.Calculations).
                       HasForeignKey<Coefficients>(c => c.CalculationsId);

                entity.HasOne(с => с.CounterValues).
                       WithOne(с => с.Calculations).
                       HasForeignKey<CounterValues>(c => c.CalculationsId);
            });

            modelBuilder.Entity<CounterValues>().ToTable("HistoryCounterValues");
            modelBuilder.Entity<CounterValues>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Coefficients>().ToTable("HistoryCoefficients");
            modelBuilder.Entity<Coefficients>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
}
