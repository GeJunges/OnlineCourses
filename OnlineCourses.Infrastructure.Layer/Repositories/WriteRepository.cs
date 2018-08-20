using OnlineCourses.Domain.Layer.Interfaces;
using OnlineCourses.Domain.Layer.Entities;
using OnlineCourses.Infrastructure.Layer.ContextConfiguration;
using System.Threading.Tasks;

namespace OnlineCourses.Infrastructure.Layer.Repositories {
    public class WriteRepository<T> : IWriteRepository<T> where T : IEntity {

        private readonly AppDbContext _context;

        public WriteRepository(AppDbContext context) {
            _context = context;
        }

        public void Save(T entity) {
            _context.Add<T>(entity);
            _context.SaveChanges();
        }

        public async Task SaveAsync(T entity) {
            await _context.AddAsync<T>(entity);
            await _context.SaveChangesAsync();
        }
    }
}
