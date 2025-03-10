using Courses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Data.Configuration
{
    public class Relationships
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            Course(modelBuilder.Entity<Course>());
            Enrollment(modelBuilder.Entity<Enrollment>());
            Instructor(modelBuilder.Entity<Instructor>());
            Department(modelBuilder.Entity<Department>());
            Message(modelBuilder.Entity<Message>());

        }
        private static void Course(EntityTypeBuilder<Course> builder)
        {
            builder.HasMany(c => c.Enrollments)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseID)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.Users)
                .WithMany(u => u.Courses)

                .UsingEntity<CourseUser>(
                    j => j.HasOne(cu => cu.User)
                        .WithMany(u => u.CourseUsers)
                        .HasForeignKey(cu => cu.UserID)
                        .OnDelete(DeleteBehavior.SetNull),

                    j => j.HasOne(cu => cu.Course)
                        .WithMany(c => c.CourseUsers)
                        .HasForeignKey(cu => cu.CourseID)
                        .OnDelete(DeleteBehavior.SetNull),

                    j =>
                    {
                        j.HasKey(cu => cu.ID);
                        j.ToTable("CourseUsers");
                        j.Property(e => e.rating).IsRequired(false);
                    });

        }
        private static void Enrollment(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasOne(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserID)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.SetNull);
        }
        private static void Instructor(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasMany(i => i.Courses)
                .WithOne(c => c.Instructor)
                .HasForeignKey(c => c.InstructorID)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(i => i.User).WithOne(u => u.Instructor).HasForeignKey<Instructor>(i => i.UserId);

        }
        private static void Department(EntityTypeBuilder<Department> builder)
        {
            builder.HasMany(d => d.Courses)
                .WithOne(c => c.Department)
                .HasForeignKey(c => c.DepartmentID)
                .OnDelete(DeleteBehavior.SetNull);
        }
        private static void Message(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne(m => m.Sender)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Receiver)
            .WithMany(u => u.ReceivedMessages)
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
