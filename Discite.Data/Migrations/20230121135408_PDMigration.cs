using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Discite.Data.Migrations
{
    public partial class PDMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "user");

            migrationBuilder.RenameColumn(
                name: "RegisterDate",
                table: "user",
                newName: "register_date");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "user",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "artifact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    upgrade_level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_artifact", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "class",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaxHp = table.Column<float>(type: "float", nullable: false),
                    Damage = table.Column<float>(type: "float", nullable: false),
                    Energy = table.Column<float>(type: "float", nullable: false),
                    Speed = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_class", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "enemy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaxHp = table.Column<float>(type: "float", nullable: false),
                    Damage = table.Column<float>(type: "float", nullable: false),
                    Energy = table.Column<float>(type: "float", nullable: false),
                    Speed = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enemy", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "eventroom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventroom", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "weapon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Damage = table.Column<float>(type: "float", nullable: false),
                    AttackSpeed = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weapon", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "artifact",
                columns: new[] { "Id", "upgrade_level", "Name" },
                values: new object[,]
                {
                    { 1, 3, "Flammable blood" },
                    { 2, 3, "Poisonous blood" },
                    { 3, 3, "Exploding corpses" },
                    { 4, 3, "Revenge damage" }
                });

            migrationBuilder.InsertData(
                table: "class",
                columns: new[] { "Id", "Damage", "Energy", "MaxHp", "Name", "Speed" },
                values: new object[,]
                {
                    { 1, 5f, 160f, 70f, "Artificier", 1f },
                    { 2, 4f, 100f, 120f, "Weapon Master", 1f },
                    { 3, 4f, 130f, 90f, "Cyber Ninja", 2f }
                });

            migrationBuilder.InsertData(
                table: "enemy",
                columns: new[] { "Id", "Damage", "Energy", "MaxHp", "Name", "Speed" },
                values: new object[,]
                {
                    { 1, 10f, 0f, 20f, "Ghoul", 0.7f },
                    { 2, 25f, 0f, 5f, "Exploder", 1f },
                    { 3, 20f, 0f, 35f, "Cyber Ghoul", 1.5f },
                    { 4, 10f, 0f, 60f, "Multi-tank", 0.5f },
                    { 5, 15f, 0f, 20f, "Agent", 0.7f },
                    { 6, 15f, 0f, 200f, "Chimera", 1.2f }
                });

            migrationBuilder.InsertData(
                table: "eventroom",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Entry room" },
                    { 2, "Exit room" },
                    { 3, "Encounter room" },
                    { 4, "Shop room" },
                    { 5, "Boss room" }
                });

            migrationBuilder.InsertData(
                table: "weapon",
                columns: new[] { "Id", "AttackSpeed", "Damage", "Name" },
                values: new object[,]
                {
                    { 1, 1.5f, 3f, "Katana" },
                    { 2, 1f, 4f, "Spear" },
                    { 3, 0.7f, 6f, "Deagle" },
                    { 4, 3f, 1f, "Laser SMG" },
                    { 5, 0.5f, 1f, "Shotgun" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_Email",
                table: "user",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "artifact");

            migrationBuilder.DropTable(
                name: "class");

            migrationBuilder.DropTable(
                name: "enemy");

            migrationBuilder.DropTable(
                name: "eventroom");

            migrationBuilder.DropTable(
                name: "weapon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_Email",
                table: "user");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "register_date",
                table: "Users",
                newName: "RegisterDate");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
