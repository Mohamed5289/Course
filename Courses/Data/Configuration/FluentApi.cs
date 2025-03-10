using Courses.Models;
using Courses.Models.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Data.Configuration
{
    public class FluentApi
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            Course(modelBuilder.Entity<Course>());
            Enrollment(modelBuilder.Entity<Enrollment>());
            Instructor(modelBuilder.Entity<Instructor>());
            Department(modelBuilder.Entity<Department>());
            User(modelBuilder.Entity<AppUser>());
            Message(modelBuilder.Entity<Message>());

        }

        private static void User(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(u => u.FirstName).HasMaxLength(50);
            builder.Property(u => u.LastName).HasMaxLength(50);
            builder.Property(u => u.SSN).HasMaxLength(14).IsRequired(false);
            builder.Property(u => u.Gender).HasMaxLength(nameof(Gender.Female).Length).IsRequired(false);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.UserName).IsUnique();
        }
        private static void Course(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            builder.HasKey(c => c.ID);
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.HasIndex(c => new {c.InstructorID , c.Name}).IsUnique();
        }
        private static void Enrollment(EntityTypeBuilder<Enrollment> builder)
        {
            builder.ToTable("Enrollments");
            builder.HasKey(e => e.ID);
            builder.Property(e => e.Status).IsRequired().HasMaxLength(20);
        }
        private static void Instructor(EntityTypeBuilder<Instructor> builder)
        {
            builder.ToTable("Instructors");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.JobTitle).HasMaxLength(50).IsRequired(false);
            builder.Property(i => i.Salary).HasColumnType("decimal(18,2)");
            builder.HasIndex(i => i.UserId).IsUnique();
            
        }
        private static void Department(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.HasKey(d => d.ID);
            builder.Property(d => d.Name).HasMaxLength(50).IsRequired();
            builder.Property(d => d.Description).HasMaxLength(100).IsRequired(false);
            builder.HasIndex(d => d.Name).IsUnique();
        }

        private static void Message(EntityTypeBuilder<Message> builder) 
        {
            builder.ToTable($"{nameof(Message)}");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Content).HasMaxLength(50).IsRequired();
        }
    }
}
