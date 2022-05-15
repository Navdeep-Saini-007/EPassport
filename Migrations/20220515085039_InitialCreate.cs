using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EPassport.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistrationDetail",
                columns: table => new
                {
                    LoginId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationDetail", x => x.LoginId);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationDetail",
                columns: table => new
                {
                    ApplicantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmploymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EducationalQualification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationDetail", x => x.ApplicantId);
                    table.ForeignKey(
                        name: "FK_ApplicationDetail_RegistrationDetail_LoginId",
                        column: x => x.LoginId,
                        principalTable: "RegistrationDetail",
                        principalColumn: "LoginId");
                });

            migrationBuilder.CreateTable(
                name: "AddressDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pincode = table.Column<int>(type: "int", nullable: false),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressDetail_ApplicationDetail_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "ApplicationDetail",
                        principalColumn: "ApplicantId");
                });

            migrationBuilder.CreateTable(
                name: "AppointmentDetail",
                columns: table => new
                {
                    MonthId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonthName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvailableDays = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeSlots = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentDetail", x => x.MonthId);
                    table.ForeignKey(
                        name: "FK_AppointmentDetail_ApplicationDetail_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "ApplicationDetail",
                        principalColumn: "ApplicantId");
                });

            migrationBuilder.CreateTable(
                name: "FamilyDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FathersName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MothersName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyDetail_ApplicationDetail_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "ApplicationDetail",
                        principalColumn: "ApplicantId");
                });

            migrationBuilder.CreateTable(
                name: "PassportOffice",
                columns: table => new
                {
                    OfficeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Jurisdiction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassportOffice", x => x.OfficeId);
                    table.ForeignKey(
                        name: "FK_PassportOffice_ApplicationDetail_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "ApplicationDetail",
                        principalColumn: "ApplicantId");
                });

            migrationBuilder.CreateTable(
                name: "ReferenceDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferenceDetail_ApplicationDetail_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "ApplicationDetail",
                        principalColumn: "ApplicantId");
                });

            migrationBuilder.CreateTable(
                name: "SupportingDocumentDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document1 = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Document2 = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportingDocumentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportingDocumentDetail_ApplicationDetail_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "ApplicationDetail",
                        principalColumn: "ApplicantId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressDetail_ApplicantId",
                table: "AddressDetail",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDetail_LoginId",
                table: "ApplicationDetail",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDetail_ApplicantId",
                table: "AppointmentDetail",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyDetail_ApplicantId",
                table: "FamilyDetail",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_PassportOffice_ApplicantId",
                table: "PassportOffice",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceDetail_ApplicantId",
                table: "ReferenceDetail",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportingDocumentDetail_ApplicantId",
                table: "SupportingDocumentDetail",
                column: "ApplicantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressDetail");

            migrationBuilder.DropTable(
                name: "AppointmentDetail");

            migrationBuilder.DropTable(
                name: "FamilyDetail");

            migrationBuilder.DropTable(
                name: "PassportOffice");

            migrationBuilder.DropTable(
                name: "ReferenceDetail");

            migrationBuilder.DropTable(
                name: "SupportingDocumentDetail");

            migrationBuilder.DropTable(
                name: "ApplicationDetail");

            migrationBuilder.DropTable(
                name: "RegistrationDetail");
        }
    }
}
