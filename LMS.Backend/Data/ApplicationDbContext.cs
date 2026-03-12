using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LMS.Backend.Models;

namespace LMS.Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<LectureMaterial> LectureMaterials { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Submission> Submissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            // Configure Course entity
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Description)
                    .HasMaxLength(1000);

                entity.HasOne(e => e.Teacher)
                    .WithMany(u => u.CoursesCreated)
                    .HasForeignKey(e => e.TeacherId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.TeacherId);
            });

            // Configure Enrollment entity
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Student)
                    .WithMany(u => u.Enrollments)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Course)
                    .WithMany(c => c.Enrollments)
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => new { e.StudentId, e.CourseId }).IsUnique();
            });

            // Configure LectureMaterial entity
            modelBuilder.Entity<LectureMaterial>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FileUrl)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(1000);

                entity.HasOne(e => e.Course)
                    .WithMany(c => c.LectureMaterials)
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.CourseId);
            });

            // Configure Assignment entity
            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Description)
                    .HasMaxLength(2000);

                entity.HasOne(e => e.Course)
                    .WithMany(c => c.Assignments)
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.CourseId);
            });

            // Configure Submission entity
            modelBuilder.Entity<Submission>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FileUrl)
                    .IsRequired();

                entity.Property(e => e.Feedback)
                    .HasMaxLength(2000);

                entity.Property(e => e.Grade)
                    .HasPrecision(5, 2);

                entity.HasOne(e => e.Assignment)
                    .WithMany(a => a.Submissions)
                    .HasForeignKey(e => e.AssignmentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Student)
                    .WithMany(u => u.Submissions)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => new { e.AssignmentId, e.StudentId }).IsUnique();
            });
        }
    }
}
