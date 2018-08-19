using Microsoft.EntityFrameworkCore;
using OnlineCourses.Domain.Layer.Entities;
using OnlineCourses.Domain.Layer.Interfaces;
using OnlineCourses.Infrastructure.Layer.ContextConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineCourses.Infrastructure.Layer.Repositories {
    public class ReadRepository<T> : IReadRepository<T> where T : IEntity {

        private readonly AppDbContext _context;

        public ReadRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<T>> FindAll(string include) {
            return await _context.Set<T>().Include(include).ToListAsync();
        }

        public async Task<T> FindById(Guid id, string include) {
            return await _context.Set<T>().Include(include).Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public T FindSingleBy(Expression<Func<T, bool>> predicate, string include) {
            return _context.Set<T>().Include(include).Where(predicate).SingleOrDefault();
        }

        public async Task<T> FindSingleByAsync(Expression<Func<T, bool>> predicate, string include) {
            return await _context.Set<T>().Include(include).Where(predicate).SingleOrDefaultAsync();
        }
    }
}
