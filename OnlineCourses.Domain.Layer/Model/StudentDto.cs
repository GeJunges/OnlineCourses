using System.ComponentModel.DataAnnotations;

namespace OnlineCourses.Domain.Layer.Model {
    public class StudentDto {

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public CourseDto CourseDto { get; set; }
    }
}
