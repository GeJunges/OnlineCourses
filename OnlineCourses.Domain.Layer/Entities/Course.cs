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
