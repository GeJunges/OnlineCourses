using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCourses.Infrastructure.Layer.Migrations {
    public partial class CreateViewCourseDetails : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            var sql = @"CREATE VIEW VW_COURSE_DETAILS
	                    AS
	                      SELECT 
		                      C.Id,
		                      C.Name, 
		                      C.TeacherId,
                              T.Name TeacherName, 
		                      C.MaximumSignatures, 
		                      max(S.Age) as MaximumAge, 
		                      avg(S.Age) as AverageAge, 
		                      min(S.Age) as MinimumAge, 
		                      count(s.CourseId) TotalSignatures
                          FROM  [dbo].[Courses] C
		                    INNER JOIN [dbo].[Teachers] T on T.Id = C.TeacherId
		                    LEFT JOIN [dbo].[Students] S on S.CourseId = C.Id	 
	                      GROUP BY C.Id,  C.Name, C.MaximumSignatures, T.Name, C.TeacherId
	                      GO";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
        }
    }
}
