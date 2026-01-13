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

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<ItemName> ItemNames { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Payment> Payments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Prevent cascade cycle
            modelBuilder.Entity<Interest>()
                .HasOne(i => i.Buyer)
                .WithMany(u => u.Interests)
                .HasForeignKey(i => i.BuyerId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Item>()
               .HasOne(i => i.ItemType)
               .WithMany()
               .HasForeignKey(i => i.ItemTypeId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.ItemName)
                .WithMany()
                .HasForeignKey(i => i.ItemNameId)
                .OnDelete(DeleteBehavior.Cascade);




           



            // Item price
            modelBuilder.Entity<Item>()
                .Property(i => i.PriceUnit)
                .HasPrecision(18, 2);

            // Payment amount
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);


            modelBuilder.Entity<Payment>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne<Item>()
                .WithMany()
                .HasForeignKey(p => p.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

        }


       



    }
}
