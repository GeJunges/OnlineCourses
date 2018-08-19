using OnlineCourses.Domain.Layer.Entities;
using System.Threading.Tasks;

namespace OnlineCourses.Domain.Layer.Interfaces {
    public interface IQueueService<T> where T: IEntity {
        void Save(T entity);
        Task SaveAsync(T entity);
    }
}
