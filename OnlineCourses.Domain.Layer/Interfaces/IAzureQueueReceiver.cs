using OnlineCourses.Domain.Layer.AzureHelpers;
using OnlineCourses.Domain.Layer.Entities;
using System;

namespace OnlineCourses.Domain.Layer.Interfaces {
    public interface IAzureQueueReceiver {
        void Receive(Func<Student, MessageProcessResponse> onProcess,
            Action<Exception> onError,
            Action onWait);
    }
}
