using System;

namespace OnlineCourses.Domain.Layer.Entities {
    public class Vw_Course_Details : IEntity {

        public virtual string Name { get; }
        public virtual int MaximumSignatures { get; }
        public virtual Guid TeacherId { get; }
        public virtual string TeacherName { get; }
        public virtual int TotalSignatures { get; }
        public virtual int MinimumAge { get; }
        public virtual int MaximumAge { get; }
        public virtual int AverageAge { get; }
    }
}
