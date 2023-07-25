using System;
using System.Collections.Generic;
using Databasefirestapproc.Models;
using Microsoft.EntityFrameworkCore;

namespace Databasefirestapproc.Context;

public partial class ProjectDBContext : DbContext
{
    public ProjectDBContext()
    {
    }

    public ProjectDBContext(DbContextOptions<ProjectDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Projectmodule> Projectmodules { get; set; }

    public virtual DbSet<Projecttask> Projecttasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-8E42IS4\\SQLEXPRESS;Initial Catalog=Projectmaneger;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Employees");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Projects");

            entity.HasOne(d => d.Maneger).WithMany(p => p.Projects).HasConstraintName("FK_Projects_Employees");
        });

        modelBuilder.Entity<Projectmodule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Projectmodules");

            entity.HasOne(d => d.Project).WithMany(p => p.Projectmodules).HasConstraintName("FK_Projectmodules_Projects");
        });

        modelBuilder.Entity<Projecttask>(entity =>
        {
            entity.HasOne(d => d.Employee).WithMany(p => p.Projecttasks).HasConstraintName("FK_projecttask_Employees");

            entity.HasOne(d => d.Module).WithMany(p => p.Projecttasks).HasConstraintName("FK_projecttask_Projectmodules");

            entity.HasOne(d => d.Project).WithMany(p => p.Projecttasks).HasConstraintName("FK_projecttask_Projects");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
