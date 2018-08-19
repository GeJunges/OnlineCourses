using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace OnlineCourses.Infrastructure.Layer.Migrations {
    public partial class SeedInitialData : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {

            var srSmith = Guid.NewGuid();
            var morpheus = Guid.NewGuid();

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Name" },
                values: new object[] { srSmith, "Sr Smith" });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Name" },
                values: new object[] { morpheus, "Sr Morpheus" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Name", "MaximumSignatures", "TeacherId" },
                values: new object[] { Guid.NewGuid(), "TDD", 10, srSmith });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Name", "MaximumSignatures", "TeacherId" },
                values: new object[] { Guid.NewGuid(), "Azure", 5, morpheus });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Name", "MaximumSignatures", "TeacherId" },
                values: new object[] { Guid.NewGuid(), ".Net Core", 3, srSmith });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Name", "MaximumSignatures", "TeacherId" },
                values: new object[] { Guid.NewGuid(), "C#", 7, morpheus });
        }

        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
