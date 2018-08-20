using Microsoft.EntityFrameworkCore;
using OnlineCourses.Domain.Layer.Entities;

namespace OnlineCourses.Infrastructure.Layer.ContextConfiguration {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Student>();
            modelBuilder.Entity<Teacher>();
            modelBuilder.Entity<Course>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
