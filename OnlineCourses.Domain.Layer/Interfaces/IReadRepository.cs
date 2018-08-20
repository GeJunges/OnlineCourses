using OnlineCourses.Domain.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineCourses.Domain.Layer.Interfaces {
    public interface IReadRepository<T> where T : IEntity {

        Task<IEnumerable<T>> FindAll(string include);
        T FindSingleBy(Expression<Func<T, bool>> predicate, string include);
        Task<T> FindById(Guid id, string[] includes);
        Task<T> FindSingleByAsync(Expression<Func<T, bool>> predicate, string includes);

    }
}
