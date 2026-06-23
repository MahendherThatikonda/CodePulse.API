using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CodePulse.API.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class RemoveAdminUserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "086d0acd-9b07-43ff-8e04-62559b712379", "5fe58d0f-e37c-44a5-8130-30c948094726" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6892ad46-648f-4d76-b9fd-83ed1654b250", "5fe58d0f-e37c-44a5-8130-30c948094726" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5fe58d0f-e37c-44a5-8130-30c948094726");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5fe58d0f-e37c-44a5-8130-30c948094726", 0, "9b28e795-269e-426e-87b2-da22a19ee386", "admin@codepulse.com", false, false, null, "ADMIN@CODEPULSE.COM", "ADMIN@CODEPULSE.COM", "AQAAAAIAAYagAAAAEGIP4HsEvFmtbxZsq3HNPZD5w7hwNzNHSB282ypXnqsbpluSYqFyPaViu4nwxOr2Rw==", null, false, "693a78a3-206d-4e12-9f17-330dfa10e5f1", false, "admin@codepulse.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "086d0acd-9b07-43ff-8e04-62559b712379", "5fe58d0f-e37c-44a5-8130-30c948094726" },
                    { "6892ad46-648f-4d76-b9fd-83ed1654b250", "5fe58d0f-e37c-44a5-8130-30c948094726" }
                });
        }
    }
}
