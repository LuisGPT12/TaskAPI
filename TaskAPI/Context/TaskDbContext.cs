using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Models;

namespace TaskAPI.Context;

public partial class TaskDbContext : DbContext
{
    public TaskDbContext()
    {
    }

    public TaskDbContext(DbContextOptions<TaskDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Models.Task> Tasks { get; set; }
    public virtual DbSet<Models.Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Task).WithMany(p => p.Items)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK_Items_Tasks");
        });

        modelBuilder.Entity<Models.Task>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
