using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuGet.Next.DM.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Key = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("Dm:Identity", "1, 1"),
                    Id = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    Authors = table.Column<string>(type: "NVARCHAR2(4000)", maxLength: 4000, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(4000)", maxLength: 4000, nullable: true),
                    Downloads = table.Column<long>(type: "BIGINT", nullable: false),
                    HasReadme = table.Column<bool>(type: "BIT", nullable: false),
                    HasEmbeddedIcon = table.Column<bool>(type: "BIT", nullable: false),
                    IsPrerelease = table.Column<bool>(type: "BIT", nullable: false),
                    ReleaseNotes = table.Column<string>(type: "NVARCHAR2(32767)", nullable: true),
                    Language = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    Listed = table.Column<bool>(type: "BIT", nullable: false),
                    MinClientVersion = table.Column<string>(type: "NVARCHAR2(44)", maxLength: 44, nullable: true),
                    Published = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    RequireLicenseAcceptance = table.Column<bool>(type: "BIT", nullable: false),
                    SemVerLevel = table.Column<int>(type: "INT", nullable: false),
                    Summary = table.Column<string>(type: "NVARCHAR2(4000)", maxLength: 4000, nullable: true),
                    Title = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: true),
                    IconUrl = table.Column<string>(type: "NVARCHAR2(4000)", maxLength: 4000, nullable: true),
                    LicenseUrl = table.Column<string>(type: "NVARCHAR2(4000)", maxLength: 4000, nullable: true),
                    ProjectUrl = table.Column<string>(type: "NVARCHAR2(4000)", maxLength: 4000, nullable: true),
                    RepositoryUrl = table.Column<string>(type: "NVARCHAR2(4000)", maxLength: 4000, nullable: true),
                    RepositoryType = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    Tags = table.Column<string>(type: "NVARCHAR2(4000)", maxLength: 4000, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "BINARY(8)", rowVersion: true, nullable: true),
                    Version = table.Column<string>(type: "NVARCHAR2(64)", maxLength: 64, nullable: false),
                    OriginalVersion = table.Column<string>(type: "NVARCHAR2(64)", maxLength: 64, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    Creator = table.Column<string>(type: "NVARCHAR2(32767)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    Modifier = table.Column<string>(type: "NVARCHAR2(32767)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "PackageUpdateRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("Dm:Identity", "1, 1"),
                    PackageId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Version = table.Column<string>(type: "NVARCHAR2(32767)", nullable: false),
                    OperationType = table.Column<string>(type: "NVARCHAR2(32767)", nullable: false),
                    OperationDescription = table.Column<string>(type: "NVARCHAR2(32767)", nullable: false),
                    OperationIP = table.Column<string>(type: "NVARCHAR2(32767)", nullable: false),
                    UserId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    OperationTime = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageUpdateRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    FullName = table.Column<string>(type: "NVARCHAR2(32767)", nullable: false),
                    Username = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(32767)", nullable: true),
                    PasswordHash = table.Column<string>(type: "NVARCHAR2(32767)", nullable: true),
                    Password = table.Column<string>(type: "NVARCHAR2(32767)", nullable: false),
                    Avatar = table.Column<string>(type: "NVARCHAR2(32767)", nullable: true),
                    Role = table.Column<string>(type: "NVARCHAR2(32767)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackageDependencies",
                columns: table => new
                {
                    Key = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("Dm:Identity", "1, 1"),
                    Id = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: true),
                    VersionRange = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: true),
                    TargetFramework = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: true),
                    PackageId = table.Column<string>(type: "NVARCHAR2(32767)", nullable: false),
                    PackageKey = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageDependencies", x => x.Key);
                    table.ForeignKey(
                        name: "FK_PackageDependencies_Packages_PackageKey",
                        column: x => x.PackageKey,
                        principalTable: "Packages",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageTypes",
                columns: table => new
                {
                    Key = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("Dm:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(512)", maxLength: 512, nullable: true),
                    Version = table.Column<string>(type: "NVARCHAR2(64)", maxLength: 64, nullable: true),
                    PackageKey = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageTypes", x => x.Key);
                    table.ForeignKey(
                        name: "FK_PackageTypes_Packages_PackageKey",
                        column: x => x.PackageKey,
                        principalTable: "Packages",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetFrameworks",
                columns: table => new
                {
                    Key = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("Dm:Identity", "1, 1"),
                    Moniker = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: true),
                    PackageId = table.Column<string>(type: "NVARCHAR2(32767)", nullable: false),
                    PackageKey = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetFrameworks", x => x.Key);
                    table.ForeignKey(
                        name: "FK_TargetFrameworks_Packages_PackageKey",
                        column: x => x.PackageKey,
                        principalTable: "Packages",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserKeys",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Key = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "DATETIME WITH TIME ZONE", nullable: false),
                    UserId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserKeys_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Avatar", "Email", "FullName", "Password", "PasswordHash", "Role", "Username" },
                values: new object[] { "8e9a24c9-30b0-4f77-beef-2131e8262d50", "https://avatars.githubusercontent.com/u/61819790?v=4", "239573049@qq.com", "token", "2f194ad739432c03baacfdbc66f426df", "5a865ebec08f40258c50bdd25cc867d2", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "UserKeys",
                columns: new[] { "Id", "CreatedTime", "Enabled", "Key", "UserId" },
                values: new object[] { "9072affbfc24487ca50d12da06ff0b02", new DateTimeOffset(new DateTime(2024, 11, 2, 2, 36, 52, 518, DateTimeKind.Unspecified).AddTicks(1468), new TimeSpan(0, 8, 0, 0, 0)), true, "key-f38050df96ba48beaa273d28f0f99de9", "8e9a24c9-30b0-4f77-beef-2131e8262d50" });

            migrationBuilder.CreateIndex(
                name: "IX_PackageDependencies_Id",
                table: "PackageDependencies",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PackageDependencies_PackageKey",
                table: "PackageDependencies",
                column: "PackageKey");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_Id",
                table: "Packages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_Id_Version",
                table: "Packages",
                columns: new[] { "Id", "Version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PackageTypes_Name",
                table: "PackageTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_PackageTypes_PackageKey",
                table: "PackageTypes",
                column: "PackageKey");

            migrationBuilder.CreateIndex(
                name: "IX_PackageUpdateRecords_PackageId",
                table: "PackageUpdateRecords",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageUpdateRecords_UserId",
                table: "PackageUpdateRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetFrameworks_Moniker",
                table: "TargetFrameworks",
                column: "Moniker");

            migrationBuilder.CreateIndex(
                name: "IX_TargetFrameworks_PackageKey",
                table: "TargetFrameworks",
                column: "PackageKey");

            migrationBuilder.CreateIndex(
                name: "IX_UserKeys_Key",
                table: "UserKeys",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserKeys_UserId",
                table: "UserKeys",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackageDependencies");

            migrationBuilder.DropTable(
                name: "PackageTypes");

            migrationBuilder.DropTable(
                name: "PackageUpdateRecords");

            migrationBuilder.DropTable(
                name: "TargetFrameworks");

            migrationBuilder.DropTable(
                name: "UserKeys");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
