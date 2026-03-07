using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class projectmine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminMetrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalKgCollected = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    WasteReducedKg = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CalculatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminMetrics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppleGrades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppleGrades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppleTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    EmailConfirmationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppleVarieties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppleTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppleVarieties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppleVarieties_AppleTypes_AppleTypeId",
                        column: x => x.AppleTypeId,
                        principalTable: "AppleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyEntities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAvailabilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    WeekNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAvailabilities_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplePrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppleVarietyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppleGradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppleTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PricePerKg = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplePrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplePrices_AppleGrades_AppleGradeId",
                        column: x => x.AppleGradeId,
                        principalTable: "AppleGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplePrices_AppleTypes_AppleTypeId",
                        column: x => x.AppleTypeId,
                        principalTable: "AppleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplePrices_AppleVarieties_AppleVarietyId",
                        column: x => x.AppleVarietyId,
                        principalTable: "AppleVarieties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollectionRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppleOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppleVarietyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityKg = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeekNumber = table.Column<int>(type: "int", nullable: false),
                    PreferredPickupAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionRequests_AppleVarieties_AppleVarietyId",
                        column: x => x.AppleVarietyId,
                        principalTable: "AppleVarieties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionRequests_Users_AppleOwnerId",
                        column: x => x.AppleOwnerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyApplePrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppleGradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PricePerKg = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppleVarietyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyApplePrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyApplePrices_AppleGrades_AppleGradeId",
                        column: x => x.AppleGradeId,
                        principalTable: "AppleGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyApplePrices_AppleVarieties_AppleVarietyId",
                        column: x => x.AppleVarietyId,
                        principalTable: "AppleVarieties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyAppleRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppleGradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityKg = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AppleVarietyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsReceived = table.Column<bool>(type: "bit", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsPaymentConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAppleRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyAppleRequests_AppleGrades_AppleGradeId",
                        column: x => x.AppleGradeId,
                        principalTable: "AppleGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyAppleRequests_AppleVarieties_AppleVarietyId",
                        column: x => x.AppleVarietyId,
                        principalTable: "AppleVarieties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyAppleRequests_CompanyEntities_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "CompanyEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyStocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppleGradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityKg = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyStocks_AppleGrades_AppleGradeId",
                        column: x => x.AppleGradeId,
                        principalTable: "AppleGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyStocks_CompanyEntities_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "CompanyEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyUsageReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppleGradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityKg = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUsageReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyUsageReports_AppleGrades_AppleGradeId",
                        column: x => x.AppleGradeId,
                        principalTable: "AppleGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyUsageReports_CompanyEntities_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "CompanyEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollectionResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CollectionRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppleVarietyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppleGradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityKg = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionResults_AppleGrades_AppleGradeId",
                        column: x => x.AppleGradeId,
                        principalTable: "AppleGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionResults_AppleVarieties_AppleVarietyId",
                        column: x => x.AppleVarietyId,
                        principalTable: "AppleVarieties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionResults_CollectionRequests_CollectionRequestId",
                        column: x => x.CollectionRequestId,
                        principalTable: "CollectionRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CollectionRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_CollectionRequests_CollectionRequestId",
                        column: x => x.CollectionRequestId,
                        principalTable: "CollectionRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CollectionRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CollectedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAssignments_CollectionRequests_CollectionRequestId",
                        column: x => x.CollectionRequestId,
                        principalTable: "CollectionRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentAssignments_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplePrices_AppleGradeId",
                table: "ApplePrices",
                column: "AppleGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplePrices_AppleTypeId_AppleVarietyId_AppleGradeId",
                table: "ApplePrices",
                columns: new[] { "AppleTypeId", "AppleVarietyId", "AppleGradeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplePrices_AppleVarietyId",
                table: "ApplePrices",
                column: "AppleVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppleVarieties_AppleTypeId",
                table: "AppleVarieties",
                column: "AppleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionRequests_AppleOwnerId",
                table: "CollectionRequests",
                column: "AppleOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionRequests_AppleVarietyId",
                table: "CollectionRequests",
                column: "AppleVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionResults_AppleGradeId",
                table: "CollectionResults",
                column: "AppleGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionResults_AppleVarietyId",
                table: "CollectionResults",
                column: "AppleVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionResults_CollectionRequestId",
                table: "CollectionResults",
                column: "CollectionRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyApplePrices_AppleGradeId",
                table: "CompanyApplePrices",
                column: "AppleGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyApplePrices_AppleVarietyId",
                table: "CompanyApplePrices",
                column: "AppleVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAppleRequests_AppleGradeId",
                table: "CompanyAppleRequests",
                column: "AppleGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAppleRequests_AppleVarietyId",
                table: "CompanyAppleRequests",
                column: "AppleVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAppleRequests_CompanyId",
                table: "CompanyAppleRequests",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEntities_UserId",
                table: "CompanyEntities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStocks_AppleGradeId",
                table: "CompanyStocks",
                column: "AppleGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStocks_CompanyId",
                table: "CompanyStocks",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUsageReports_AppleGradeId",
                table: "CompanyUsageReports",
                column: "AppleGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUsageReports_CompanyId",
                table: "CompanyUsageReports",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CollectionRequestId",
                table: "Payments",
                column: "CollectionRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OwnerId",
                table: "Payments",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssignments_CollectionRequestId",
                table: "StudentAssignments",
                column: "CollectionRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssignments_StudentId",
                table: "StudentAssignments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAvailabilities_StudentId",
                table: "StudentAvailabilities",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminMetrics");

            migrationBuilder.DropTable(
                name: "ApplePrices");

            migrationBuilder.DropTable(
                name: "CollectionResults");

            migrationBuilder.DropTable(
                name: "CompanyApplePrices");

            migrationBuilder.DropTable(
                name: "CompanyAppleRequests");

            migrationBuilder.DropTable(
                name: "CompanyStocks");

            migrationBuilder.DropTable(
                name: "CompanyUsageReports");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "StudentAssignments");

            migrationBuilder.DropTable(
                name: "StudentAvailabilities");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "AppleGrades");

            migrationBuilder.DropTable(
                name: "CompanyEntities");

            migrationBuilder.DropTable(
                name: "CollectionRequests");

            migrationBuilder.DropTable(
                name: "AppleVarieties");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AppleTypes");
        }
    }
}
