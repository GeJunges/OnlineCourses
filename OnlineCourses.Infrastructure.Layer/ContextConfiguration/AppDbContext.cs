using Microsoft.EntityFrameworkCore;
using OnlineCourses.Domain.Layer.Entities;

namespace OnlineCourses.Infrastructure.Layer.ContextConfiguration {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Vw_Course_Details> Vw_Course_Details { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Student>();
            modelBuilder.Entity<Teacher>();
            modelBuilder.Entity<Course>();

           // modelBuilder.Entity<Vw_Course_Details>(entity => { entity.HasKey(e => e.Id); });

            base.OnModelCreating(modelBuilder);
        }
    }
}
