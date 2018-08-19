using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourses.Domain.Layer.Entities {
    public class Course : IEntity {

        [Required]
        public string Name { get; set; }

        [Required]
        public int MaximumSignatures { get; set; }

        [Required]
        public Teacher Teacher { get; set; }
                
        public virtual string TeacherName { get; }
        public virtual int TotalSignatures { get; }
        public virtual int MinimumAge { get; }
        public virtual int MaximumAge { get; }
        public virtual int AverageAge { get; }

        public List<Student> Students { get; set; }

        public virtual bool HasVacancy {
            get {
                if (Students != null && Students.Count > 0) {

                    return Students.Count < MaximumSignatures;
                }

                return true;
            }
        }
    }
}
