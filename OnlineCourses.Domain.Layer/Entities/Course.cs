using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourses.Domain.Layer.Entities {
    public class Course : IEntity {

        [Required]
        public string Name { get; set; }

        [Required]
        public int MaximumSignatures { get; set; }

        [Required]
        [JsonIgnore]
        public Teacher Teacher { get; set; }
        [JsonIgnore]
        public List<Student> Students { get; set; }
        [JsonIgnore]
        public virtual string TeacherName { get; }
        [JsonIgnore]
        public virtual int TotalSignatures { get; }
        [JsonIgnore]
        public virtual int MinimumAge { get; }
        [JsonIgnore]
        public virtual int MaximumAge { get; }
        [JsonIgnore]
        public virtual int AverageAge { get; }
        [JsonIgnore]
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
