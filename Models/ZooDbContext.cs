using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ZooAPI.Models;

public partial class ZooDbContext : DbContext
{
    public ZooDbContext()
    {
    }

    public ZooDbContext(DbContextOptions<ZooDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnimalRecord> AnimalRecords { get; set; }

    public virtual DbSet<FeedingDatum> FeedingData { get; set; }

    public virtual DbSet<FeedingInformation> FeedingInformations { get; set; }

    public virtual DbSet<Headkeeper> Headkeepers { get; set; }

    public virtual DbSet<Vet> Vets { get; set; }

    public virtual DbSet<VeterinaryRecord> VeterinaryRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MacBook-Pro\\SQLEXPRESS;Database=ZooDB1;Trusted_Connection=true; Encrypt=false; TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnimalRecord>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AnimalName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BirthPlace)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Enclosure)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ParentFemaleName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ParentMaleName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Species)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FeedingDatum>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Food)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.HasOne(d => d.Animal).WithMany(p => p.FeedingData)
                .HasForeignKey(d => d.AnimalId);
        });

        modelBuilder.Entity<FeedingInformation>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(d => d.Animal).WithMany(p => p.FeedingInformations)
                .HasForeignKey(d => d.AnimalId);
        });

        modelBuilder.Entity<Headkeeper>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vet>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VeterinaryRecord>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(d => d.Animal).WithMany(p => p.VeterinaryRecords)
                .HasForeignKey(d => d.AnimalId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
