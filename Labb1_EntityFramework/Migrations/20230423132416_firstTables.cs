using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb1_EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class firstTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfLeaves",
                columns: table => new
                {
                    LeaveTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfLeaves", x => x.LeaveTypeID);
                });

            migrationBuilder.CreateTable(
                name: "LeaveLists",
                columns: table => new
                {
                    LeaveListID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_LeaveTypeID = table.Column<int>(type: "int", nullable: false),
                    FK_EmployeeID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveLists", x => x.LeaveListID);
                    table.ForeignKey(
                        name: "FK_LeaveLists_Employees_FK_EmployeeID",
                        column: x => x.FK_EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveLists_TypeOfLeaves_FK_LeaveTypeID",
                        column: x => x.FK_LeaveTypeID,
                        principalTable: "TypeOfLeaves",
                        principalColumn: "LeaveTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveLists_FK_EmployeeID",
                table: "LeaveLists",
                column: "FK_EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveLists_FK_LeaveTypeID",
                table: "LeaveLists",
                column: "FK_LeaveTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveLists");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "TypeOfLeaves");
        }
    }
}
