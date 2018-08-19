using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OnlineCourses.Domain.Layer.Entities {
    public class Student : IEntity {

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [JsonIgnore]
        public Course Course { get; set; }
    }
}
