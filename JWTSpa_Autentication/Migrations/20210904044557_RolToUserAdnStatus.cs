using Microsoft.EntityFrameworkCore.Migrations;

namespace JWTSpa_Autentication.Migrations
{
    public partial class RolToUserAdnStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Status_StatusId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RolId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdRol",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdStatus",
                table: "Roles");

            migrationBuilder.AlterColumn<int>(
                name: "RolId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Status_StatusId",
                table: "Roles",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RolId",
                table: "Users",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Status_StatusId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RolId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "RolId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdRol",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Roles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdStatus",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Status_StatusId",
                table: "Roles",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RolId",
                table: "Users",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
