using imageupload.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace imageupload.Data
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
        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
           
        //}


    }
}
