using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options) { }

        public DbSet<AdminMetric> AdminMetrics { get; set; }
        public DbSet<AppleVariety> AppleVarieties { get; set; }
        public DbSet<AppleType> AppleTypes { get; set; }
        public DbSet<ApplePrice> ApplePrices { get; set; }
        public DbSet<AppleGrade> AppleGrades { get; set; }
        public DbSet<CollectionResult> CollectionResults { get; set; }
        public DbSet<CollectionRequest> CollectionRequests { get; set; }
        public DbSet<CompanyStock> CompanyStocks { get; set; }
        public DbSet<CompanyEntity> CompanyEntities { get; set; }
        public DbSet<CompanyAppleRequest> CompanyAppleRequests { get; set; }
        public DbSet<CompanyUsageReport> CompanyUsageReports { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        //public DbSet<AvailableGrade> AvailableGrades { get; set; }
        public DbSet<StudentAssignment> StudentAssignments { get; set; }
        public DbSet<CompanyApplePrice> CompanyApplePrices { get; set; }
        public DbSet<StudentAvailability> StudentAvailabilities { get; set; }










        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // Relationship Configurations (NO DUPLICATES)
            // =========================

            modelBuilder.Entity<CollectionResult>()
                .HasOne(cr => cr.CollectionRequest)
                .WithMany()
                .HasForeignKey(cr => cr.CollectionRequestId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentAssignment>()
                .HasOne(sa => sa.CollectionRequest)
                .WithMany()
                .HasForeignKey(sa => sa.CollectionRequestId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentAssignment>()
                .HasOne(sa => sa.Student)
                .WithMany()
                .HasForeignKey(sa => sa.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CollectionRequest>()
                .HasOne(cr => cr.User)
                .WithMany()
                .HasForeignKey(cr => cr.AppleOwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Owner)
                .WithMany()
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CompanyStock>()
                .HasOne(c => c.Company)
                .WithMany()
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);

            // =========================
            // ApplePrice - Prevent Multiple Cascade Paths
            // =========================

            modelBuilder.Entity<ApplePrice>()
                .HasOne(p => p.AppleType)
                .WithMany()
                .HasForeignKey(p => p.AppleTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplePrice>()
                .HasOne(p => p.AppleVariety)
                .WithMany()
                .HasForeignKey(p => p.AppleVarietyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplePrice>()
                .HasOne(p => p.AppleGrade)
                .WithMany()
                .HasForeignKey(p => p.AppleGradeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplePrice>()
    .HasIndex(p => new {
        p.AppleTypeId,
        p.AppleVarietyId,
        p.AppleGradeId
    })
    .IsUnique();

            // =========================
            // Decimal Precision (Fix ALL Warnings)
            // =========================

            modelBuilder.Entity<CollectionRequest>()
                .Property(x => x.QuantityKg)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CollectionResult>()
                .Property(x => x.QuantityKg)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CompanyStock>()
                .Property(x => x.QuantityKg)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CompanyAppleRequest>()
                .Property(x => x.QuantityKg)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CompanyUsageReport>()
                .Property(x => x.QuantityKg)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
                .Property(x => x.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<AdminMetric>()
                .Property(x => x.TotalKgCollected)
                .HasPrecision(18, 2);

            modelBuilder.Entity<AdminMetric>()
                .Property(x => x.WasteReducedKg)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ApplePrice>()
                .Property(p => p.PricePerKg)
                .HasPrecision(18, 2);



            modelBuilder.Entity<CompanyApplePrice>()
      .Property(p => p.PricePerKg)
      .HasPrecision(18, 2);

            modelBuilder.Entity<CompanyAppleRequest>()
                .Property(p => p.TotalAmount)
                .HasPrecision(18, 2);


        }












    }
}
