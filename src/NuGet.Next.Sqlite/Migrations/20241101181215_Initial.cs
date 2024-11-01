using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuGet.Next.Sqlite.Migrations
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
                    Key = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Id = table.Column<string>(type: "TEXT COLLATE NOCASE", maxLength: 128, nullable: false),
                    Authors = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    Downloads = table.Column<long>(type: "INTEGER", nullable: false),
                    HasReadme = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasEmbeddedIcon = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPrerelease = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReleaseNotes = table.Column<string>(type: "TEXT", nullable: true),
                    Language = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Listed = table.Column<bool>(type: "INTEGER", nullable: false),
                    MinClientVersion = table.Column<string>(type: "TEXT", maxLength: 44, nullable: true),
                    Published = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RequireLicenseAcceptance = table.Column<bool>(type: "INTEGER", nullable: false),
                    SemVerLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Summary = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    IconUrl = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    LicenseUrl = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    ProjectUrl = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    RepositoryUrl = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    RepositoryType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Tags = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true),
                    Version = table.Column<string>(type: "TEXT COLLATE NOCASE", maxLength: 64, nullable: false),
                    OriginalVersion = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Creator = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Modifier = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "PackageUpdateRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PackageId = table.Column<string>(type: "TEXT", nullable: false),
                    Version = table.Column<string>(type: "TEXT", nullable: false),
                    OperationType = table.Column<string>(type: "TEXT", nullable: false),
                    OperationDescription = table.Column<string>(type: "TEXT", nullable: false),
                    OperationIP = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    OperationTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageUpdateRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Avatar = table.Column<string>(type: "TEXT", nullable: true),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackageDependencies",
                columns: table => new
                {
                    Key = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Id = table.Column<string>(type: "TEXT COLLATE NOCASE", maxLength: 128, nullable: true),
                    VersionRange = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    TargetFramework = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    PackageId = table.Column<string>(type: "TEXT", nullable: false),
                    PackageKey = table.Column<int>(type: "INTEGER", nullable: false)
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
                    Key = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT COLLATE NOCASE", maxLength: 512, nullable: true),
                    Version = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    PackageKey = table.Column<int>(type: "INTEGER", nullable: false)
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
                    Key = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Moniker = table.Column<string>(type: "TEXT COLLATE NOCASE", maxLength: 256, nullable: true),
                    PackageId = table.Column<string>(type: "TEXT", nullable: false),
                    PackageKey = table.Column<int>(type: "INTEGER", nullable: false)
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
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false)
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
                values: new object[] { "b37b542c-2dac-4d79-85c0-5ff7419a17d5", "https://avatars.githubusercontent.com/u/61819790?v=4", "239573049@qq.com", "token", "16f0836f1e404d2cc9a88934a06c457a", "c556c15f9dcd4dc093176f8e30c207e6", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "UserKeys",
                columns: new[] { "Id", "CreatedTime", "Enabled", "Key", "UserId" },
                values: new object[] { "3a432a83f5ad4ed2aeb111f191c74e52", new DateTimeOffset(new DateTime(2024, 11, 2, 2, 12, 14, 950, DateTimeKind.Unspecified).AddTicks(2718), new TimeSpan(0, 8, 0, 0, 0)), true, "key-d25c8efb94074541873447b65f7bc7c2", "b37b542c-2dac-4d79-85c0-5ff7419a17d5" });

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
