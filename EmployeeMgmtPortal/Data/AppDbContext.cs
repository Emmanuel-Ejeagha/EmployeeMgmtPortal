using System;
using EmployeeMgmtPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMgmtPortal.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Salary)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Employee>()
            .Property(e => e.Email)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Employee>()
            .Property(e => e.PhoneNumber)
            .HasMaxLength(20);

        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.Email)
            .IsUnique();
    }
}
