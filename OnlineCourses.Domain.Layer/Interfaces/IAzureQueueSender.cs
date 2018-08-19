using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineCourses.Domain.Layer.Interfaces {
    public interface IAzureQueueSender<T> where T: class {

        Task SendAsync(T item);
        Task SendAsync(T item, Dictionary<string, object> properties);
    }
}
