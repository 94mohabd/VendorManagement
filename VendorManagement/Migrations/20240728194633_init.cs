using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VendorManagement.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    AssessmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTemplate = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.AssessmentId);
                });

            migrationBuilder.CreateTable(
                name: "AuditCycles",
                columns: table => new
                {
                    AuditCycleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditCycleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditCyclePeriod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditCycles", x => x.AuditCycleId);
                });

            migrationBuilder.CreateTable(
                name: "ContractCycles",
                columns: table => new
                {
                    ContractCycleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractCycleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractCyclePeriod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractCycles", x => x.ContractCycleId);
                });

            migrationBuilder.CreateTable(
                name: "CriticalityLevels",
                columns: table => new
                {
                    CriticalityLevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriticalityLevels", x => x.CriticalityLevelId);
                });

            migrationBuilder.CreateTable(
                name: "Headers",
                columns: table => new
                {
                    HeaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Headers", x => x.HeaderId);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTypes",
                columns: table => new
                {
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTypes", x => x.QuestionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "VendorSharedData",
                columns: table => new
                {
                    VendorSharedDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SharedData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorSharedData", x => x.VendorSharedDataId);
                });

            migrationBuilder.CreateTable(
                name: "VendorTypes",
                columns: table => new
                {
                    VendorTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorTypes", x => x.VendorTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentItem",
                columns: table => new
                {
                    AssessmentItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ItemType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: true),
                    ItemText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentItem", x => x.AssessmentItemId);
                    table.ForeignKey(
                        name: "FK_AssessmentItem_Assessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "AssessmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentItem_QuestionTypes_QuestionTypeId",
                        column: x => x.QuestionTypeId,
                        principalTable: "QuestionTypes",
                        principalColumn: "QuestionTypeId");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_QuestionTypes_QuestionTypeId",
                        column: x => x.QuestionTypeId,
                        principalTable: "QuestionTypes",
                        principalColumn: "QuestionTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentalOwnerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditDueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractDueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractCategoryStatus = table.Column<int>(type: "int", nullable: false),
                    AuditCategoryStatus = table.Column<int>(type: "int", nullable: false),
                    ContractDaysUntilExpiration = table.Column<double>(type: "float", nullable: false),
                    AuditDaysUntilExpiration = table.Column<double>(type: "float", nullable: false),
                    ContractCycleId = table.Column<int>(type: "int", nullable: false),
                    AuditCycleId = table.Column<int>(type: "int", nullable: false),
                    CriticalityLevelId = table.Column<int>(type: "int", nullable: false),
                    VendorTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorId);
                    table.ForeignKey(
                        name: "FK_Vendors_AuditCycles_AuditCycleId",
                        column: x => x.AuditCycleId,
                        principalTable: "AuditCycles",
                        principalColumn: "AuditCycleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendors_ContractCycles_ContractCycleId",
                        column: x => x.ContractCycleId,
                        principalTable: "ContractCycles",
                        principalColumn: "ContractCycleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendors_CriticalityLevels_CriticalityLevelId",
                        column: x => x.CriticalityLevelId,
                        principalTable: "CriticalityLevels",
                        principalColumn: "CriticalityLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendors_VendorTypes_VendorTypeId",
                        column: x => x.VendorTypeId,
                        principalTable: "VendorTypes",
                        principalColumn: "VendorTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentAnswers",
                columns: table => new
                {
                    AssessmentAnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentQuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentAnswers", x => x.AssessmentAnswerId);
                    table.ForeignKey(
                        name: "FK_AssessmentAnswers_AssessmentItem_AssessmentQuestionId",
                        column: x => x.AssessmentQuestionId,
                        principalTable: "AssessmentItem",
                        principalColumn: "AssessmentItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignedAssessments",
                columns: table => new
                {
                    AssignedAssessmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    AssessmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedAssessments", x => x.AssignedAssessmentId);
                    table.ForeignKey(
                        name: "FK_AssignedAssessments_Assessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "AssessmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedAssessments_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    AuditId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.AuditId);
                    table.ForeignKey(
                        name: "FK_Audits_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorVendorSharedData",
                columns: table => new
                {
                    VendorSharedDataId = table.Column<int>(type: "int", nullable: false),
                    VendorsVendorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorVendorSharedData", x => new { x.VendorSharedDataId, x.VendorsVendorId });
                    table.ForeignKey(
                        name: "FK_VendorVendorSharedData_VendorSharedData_VendorSharedDataId",
                        column: x => x.VendorSharedDataId,
                        principalTable: "VendorSharedData",
                        principalColumn: "VendorSharedDataId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorVendorSharedData_Vendors_VendorsVendorId",
                        column: x => x.VendorsVendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditVendorSharedData",
                columns: table => new
                {
                    AuditsAuditId = table.Column<int>(type: "int", nullable: false),
                    VendorSharedDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditVendorSharedData", x => new { x.AuditsAuditId, x.VendorSharedDataId });
                    table.ForeignKey(
                        name: "FK_AuditVendorSharedData_Audits_AuditsAuditId",
                        column: x => x.AuditsAuditId,
                        principalTable: "Audits",
                        principalColumn: "AuditId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuditVendorSharedData_VendorSharedData_VendorSharedDataId",
                        column: x => x.VendorSharedDataId,
                        principalTable: "VendorSharedData",
                        principalColumn: "VendorSharedDataId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AuditCycles",
                columns: new[] { "AuditCycleId", "AuditCyclePeriod", "AuditCycleType" },
                values: new object[,]
                {
                    { 1, 1, "Monthly" },
                    { 2, 3, "Quarterly" },
                    { 3, 6, "Semi-Annual" },
                    { 4, 12, "Annual" },
                    { 5, 24, "Bi-Annual" }
                });

            migrationBuilder.InsertData(
                table: "ContractCycles",
                columns: new[] { "ContractCycleId", "ContractCyclePeriod", "ContractCycleType" },
                values: new object[,]
                {
                    { 1, 1, "Monthly" },
                    { 2, 3, "Quarterly" },
                    { 3, 6, "Semi-Annual" },
                    { 4, 12, "Annual" },
                    { 5, 24, "Bi-Annual" }
                });

            migrationBuilder.InsertData(
                table: "CriticalityLevels",
                columns: new[] { "CriticalityLevelId", "Level" },
                values: new object[,]
                {
                    { 1, "Low" },
                    { 2, "Medium" },
                    { 3, "High" }
                });

            migrationBuilder.InsertData(
                table: "Headers",
                columns: new[] { "HeaderId", "HeaderText" },
                values: new object[,]
                {
                    { 1, "Header 1" },
                    { 2, "Header 2" },
                    { 3, "Header 3" }
                });

            migrationBuilder.InsertData(
                table: "QuestionTypes",
                columns: new[] { "QuestionTypeId", "Type" },
                values: new object[,]
                {
                    { 1, "Radio" },
                    { 2, "Checkbox" }
                });

            migrationBuilder.InsertData(
                table: "VendorSharedData",
                columns: new[] { "VendorSharedDataId", "LastUpdated", "SharedData" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "General" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Contact" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Financial" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ID Numbers" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Web/Geolocation" },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Human Resources" },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Health/Insurance" },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Biometric/Genetic" },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Special Category" },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Other" }
                });

            migrationBuilder.InsertData(
                table: "VendorTypes",
                columns: new[] { "VendorTypeId", "Type" },
                values: new object[,]
                {
                    { 1, "IT Hardware" },
                    { 2, "Software" },
                    { 3, "SaaS/PaaS" },
                    { 4, "Services" },
                    { 5, "Services (IT/Technical)" },
                    { 6, "Physical Security" },
                    { 7, "Other-Products" },
                    { 8, "Other-Services" },
                    { 9, "Records Information Management/Shredding" },
                    { 10, "HR/Benefits" },
                    { 11, "Insurance" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "QuestionText", "QuestionTypeId" },
                values: new object[,]
                {
                    { 1, "What is considered personally identifiable information (PII)?", 2 },
                    { 2, "How should sensitive data be handled in storage and transmission?", 1 },
                    { 3, "What steps should be taken to secure user data in a database?", 1 }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "AnswerId", "AnswerText", "QuestionId" },
                values: new object[,]
                {
                    { 1, "Social Security Number (SSN)", 1 },
                    { 2, "Date of Birth", 1 },
                    { 3, "Home Address", 1 },
                    { 4, "Encrypted at rest and in transit", 2 },
                    { 5, "Stored in plaintext", 2 },
                    { 6, "Sent over unsecured connections", 2 },
                    { 7, "Implement strong access controls", 3 },
                    { 8, "Use weak passwords", 3 },
                    { 9, "Share database credentials openly", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentAnswers_AssessmentQuestionId",
                table: "AssessmentAnswers",
                column: "AssessmentQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentItem_AssessmentId",
                table: "AssessmentItem",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentItem_QuestionTypeId",
                table: "AssessmentItem",
                column: "QuestionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedAssessments_AssessmentId",
                table: "AssignedAssessments",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedAssessments_VendorId",
                table: "AssignedAssessments",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Audits_VendorId",
                table: "Audits",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditVendorSharedData_VendorSharedDataId",
                table: "AuditVendorSharedData",
                column: "VendorSharedDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionTypeId",
                table: "Questions",
                column: "QuestionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_AuditCycleId",
                table: "Vendors",
                column: "AuditCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_ContractCycleId",
                table: "Vendors",
                column: "ContractCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_CriticalityLevelId",
                table: "Vendors",
                column: "CriticalityLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_VendorTypeId",
                table: "Vendors",
                column: "VendorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorVendorSharedData_VendorsVendorId",
                table: "VendorVendorSharedData",
                column: "VendorsVendorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "AssessmentAnswers");

            migrationBuilder.DropTable(
                name: "AssignedAssessments");

            migrationBuilder.DropTable(
                name: "AuditVendorSharedData");

            migrationBuilder.DropTable(
                name: "Headers");

            migrationBuilder.DropTable(
                name: "VendorVendorSharedData");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "AssessmentItem");

            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "VendorSharedData");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "QuestionTypes");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "AuditCycles");

            migrationBuilder.DropTable(
                name: "ContractCycles");

            migrationBuilder.DropTable(
                name: "CriticalityLevels");

            migrationBuilder.DropTable(
                name: "VendorTypes");
        }
    }
}
