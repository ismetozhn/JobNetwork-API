using System;
using System.Collections.Generic;
using JobNetworkAPI.Domain.Entities;
using JobNetworkAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using File = JobNetworkAPI.Domain.Entities.File;

namespace JobNetworkAPI.API;

public partial class JobNetworkkDbContext : DbContext
{
    public JobNetworkkDbContext()
    {
    }

    public JobNetworkkDbContext(DbContextOptions<JobNetworkkDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplyStatus> ApplyStatuses { get; set; }

    public virtual DbSet<JobApplications> JobApplications { get; set; }

    public virtual DbSet<JobPosts> JobPosts { get; set; }

    public virtual DbSet<JobTypes> JobTypes { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<UserTypes> UserTypes { get; set; }

    public DbSet<File> Files { get; set; }
    public DbSet<JobPostImageFile> JobPostImageFiles { get; set; }
    public DbSet<CvFile> CvFiles { get; set; }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
       var datas= ChangeTracker.Entries<BaseEntity>();

        foreach(var data in datas)
        {
            _ = data.State switch
            {
                EntityState.Added=>data.Entity.CreatedDate=DateTime.UtcNow,
                EntityState.Modified=>data.Entity.UpdatedDate=DateTime.UtcNow,
                _ => DateTime.UtcNow
            };
        }



        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ISMETOZHN\\SQLEXPRESS;Database=JobNetworkDb; User Id=sa; Password=1234; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplyStatus>(entity =>
        {
            entity.ToTable("ApplyStatus");

            entity.Property(e => e.ApplyStatus1)
                .HasMaxLength(200)
                .HasColumnName("ApplyStatus");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<JobApplications>(entity =>
        {
            entity.Property(e => e.ApplyDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.ApplyStatus).WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.ApplyStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobApplications_ApplyStatus");

            entity.HasOne(d => d.JobPost).WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.JobPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobApplications_JobPosts");

            entity.HasOne(d => d.User).WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobApplications_Users");
        });

        modelBuilder.Entity<JobPosts>(entity =>
        {
            entity.Property(e => e.CompanyName).HasMaxLength(250);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.JobType).WithMany(p => p.JobPosts)
                .HasForeignKey(d => d.JobTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobPosts_JobTypes");
        });

        modelBuilder.Entity<JobTypes>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Type).HasMaxLength(200);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.Property(e => e.ContactNumber).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasMaxLength(50);

            entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_UserTypes");
        });

        modelBuilder.Entity<UserTypes>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserType1)
                .HasMaxLength(50)
                .HasColumnName("UserType");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
