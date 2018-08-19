using OnlineCourses.Domain.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineCourses.Domain.Layer.Interfaces {
    public interface IReadRepository<T> where T : IEntity {

        IEnumerable<T> FindAll();
        T FindById(Guid id);
        T FindSingleBy(Expression<Func<T, bool>> predicate, string include);

        Task<T> FindSingleByAsync(Expression<Func<T, bool>> predicate, string include);
    }
}
