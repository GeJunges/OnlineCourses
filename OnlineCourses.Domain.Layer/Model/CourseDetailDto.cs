namespace OnlineCourses.Domain.Layer.Model {
    public class CourseDetailDto {

        public string Name { get; set; }
        public int MaximumSignatures { get; set; }
        public int TotalSignatures { get; set; }
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }
        public double AverageAge { get; set; }
        public TeacherDto TeacherDto { get; set; }
    }
}