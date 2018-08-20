using OnlineCourses.Domain.Layer.Entities;
using System.Threading.Tasks;

namespace OnlineCourses.Domain.Layer.Interfaces {
    public interface IPersistenceService<T> where T: IEntity {
        void Save(T entity);
        Task SaveAsync(T entity);
        Task SendToQueueToSaveAsync(T entity);
    }
}
