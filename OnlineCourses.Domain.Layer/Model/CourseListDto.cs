namespace OnlineCourses.Domain.Layer.Model {
    public class CourseListDto {

        public string Name { get; set; }
        public int MaximumSignatures { get; set; }
        public int TotalSignatures { get; set; }
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }
        public int AverageAge { get; set; }
    }
}