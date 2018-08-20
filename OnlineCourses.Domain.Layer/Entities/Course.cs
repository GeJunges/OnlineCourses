using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        public int TotalSignatures { get; set; }
        [JsonIgnore]
        public int MinimumAge { get; set; }
        [JsonIgnore]
        public int MaximumAge { get; set; }
        [JsonIgnore]
        public double AverageAge { get; set; }
        [JsonIgnore]
        public virtual bool HasVacancy {
            get {
                if (Students != null && Students.Count > 0) {

                    return Students.Count < MaximumSignatures;
                }

                return true;
            }
        }

        public void CalculateData() {
            TotalSignatures = Students.Count;
            MinimumAge = Students.Min(s => s.Age);
            MaximumAge = Students.Max(s => s.Age);
            AverageAge = Students.Average(s => s.Age);
        }
    }
}
