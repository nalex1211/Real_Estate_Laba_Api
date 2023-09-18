using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Real_Estate_API.Models;

namespace Real_Estate_API.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasIndex(e => e.ImageId, "IX_Properties_ImageId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Location).WithOne(p => p.Property).HasForeignKey<Property>(d => d.Id);

            entity.HasOne(d => d.Image).WithMany(p => p.Properties).HasForeignKey(d => d.ImageId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
