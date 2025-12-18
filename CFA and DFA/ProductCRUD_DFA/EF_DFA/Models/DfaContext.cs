using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EF_DFA.Models;

public partial class DfaContext : DbContext
{
    public DfaContext()
    {
    }

    public DfaContext(DbContextOptions<DfaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-4BTVKFHO;Initial Catalog=DFA;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC075D4F7396");

            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
