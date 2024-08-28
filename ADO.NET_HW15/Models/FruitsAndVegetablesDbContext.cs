using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ADO.NET_HW15.Models;

public partial class FruitsAndVegetablesDbContext : DbContext
{
    public FruitsAndVegetablesDbContext()
    {
    }

    public FruitsAndVegetablesDbContext(DbContextOptions<FruitsAndVegetablesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<List> List { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FruitsAndVegetablesDB;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<List>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0796AA5682");

            entity.ToTable("List");

            entity.Property(e => e.Color).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
