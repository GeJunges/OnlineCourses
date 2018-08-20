namespace OnlineCourses.Domain.Layer.Constants {
    public static class Includes {

        private static readonly string Student = "Students";
        private static readonly string Teacher = "Teacher";

        public static string[] StudentsAndTeacher() {
            return new[] {
               Student, Teacher
           };
        }

        public static string Students() => Student;
    }
}
