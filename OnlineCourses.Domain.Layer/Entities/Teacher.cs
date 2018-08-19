using System.ComponentModel.DataAnnotations;

namespace OnlineCourses.Domain.Layer.Entities {
    public class Teacher : IEntity {

        [Required]
        public string Name { get; set; }
    }
}
