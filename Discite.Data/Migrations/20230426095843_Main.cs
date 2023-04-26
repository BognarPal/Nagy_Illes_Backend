using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Discite.Data.Migrations
{
    public partial class Main : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "artifact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Power = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_artifact", x => x.Id);
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
                    Health = table.Column<float>(type: "float", nullable: false),
                    Damage = table.Column<float>(type: "float", nullable: false),
                    Speed = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enemy", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Hash = table.Column<byte[]>(type: "longblob", nullable: false),
                    Salt = table.Column<byte[]>(type: "longblob", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastActive = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
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
                    Speed = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weapon", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "run",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Runtime = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CurrentHp = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_run", x => x.Id);
                    table.ForeignKey(
                        name: "FK_run_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "run_artifact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RunId = table.Column<int>(type: "int", nullable: false),
                    ArtifactId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<int>(type: "int", nullable: false),
                    Power = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_run_artifact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_run_artifact_artifact_ArtifactId",
                        column: x => x.ArtifactId,
                        principalTable: "artifact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_run_artifact_run_RunId",
                        column: x => x.RunId,
                        principalTable: "run",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "run_enemy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RunId = table.Column<int>(type: "int", nullable: false),
                    EnemyId = table.Column<int>(type: "int", nullable: false),
                    Deaths = table.Column<int>(type: "int", nullable: false),
                    Seen = table.Column<int>(type: "int", nullable: false),
                    Damage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_run_enemy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_run_enemy_enemy_EnemyId",
                        column: x => x.EnemyId,
                        principalTable: "enemy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_run_enemy_run_RunId",
                        column: x => x.RunId,
                        principalTable: "run",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "run_weapon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RunId = table.Column<int>(type: "int", nullable: false),
                    WeaponId = table.Column<int>(type: "int", nullable: false),
                    Picked = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_run_weapon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_run_weapon_run_RunId",
                        column: x => x.RunId,
                        principalTable: "run",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_run_weapon_weapon_WeaponId",
                        column: x => x.WeaponId,
                        principalTable: "weapon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "artifact",
                columns: new[] { "Id", "Name", "Power" },
                values: new object[,]
                {
                    { 1, "Flammable blood", 3f },
                    { 2, "Poisonous blood", 3f },
                    { 3, "Exploding corpses", 3f },
                    { 4, "Revenge damage", 3f }
                });

            migrationBuilder.InsertData(
                table: "enemy",
                columns: new[] { "Id", "Damage", "Health", "Name", "Speed" },
                values: new object[,]
                {
                    { 1, 10f, 20f, "Ghoul", 0.7f },
                    { 2, 25f, 5f, "Exploder", 1f },
                    { 3, 20f, 35f, "Cyber Ghoul", 1.5f },
                    { 4, 10f, 60f, "Multi-tank", 0.5f },
                    { 5, 15f, 20f, "Agent", 0.7f },
                    { 6, 15f, 200f, "Chimera", 1.2f }
                });

            migrationBuilder.InsertData(
                table: "weapon",
                columns: new[] { "Id", "Damage", "Name", "Speed" },
                values: new object[,]
                {
                    { 1, 3f, "Katana", 1.5f },
                    { 2, 4f, "Spear", 1f },
                    { 3, 6f, "Deagle", 0.7f },
                    { 4, 1f, "Laser SMG", 3f },
                    { 5, 1f, "Shotgun", 0.5f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_run_UserId",
                table: "run",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_run_artifact_ArtifactId",
                table: "run_artifact",
                column: "ArtifactId");

            migrationBuilder.CreateIndex(
                name: "IX_run_artifact_RunId",
                table: "run_artifact",
                column: "RunId");

            migrationBuilder.CreateIndex(
                name: "IX_run_enemy_EnemyId",
                table: "run_enemy",
                column: "EnemyId");

            migrationBuilder.CreateIndex(
                name: "IX_run_enemy_RunId",
                table: "run_enemy",
                column: "RunId");

            migrationBuilder.CreateIndex(
                name: "IX_run_weapon_RunId",
                table: "run_weapon",
                column: "RunId");

            migrationBuilder.CreateIndex(
                name: "IX_run_weapon_WeaponId",
                table: "run_weapon",
                column: "WeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_user_Email",
                table: "user",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_UserName",
                table: "user",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "run_artifact");

            migrationBuilder.DropTable(
                name: "run_enemy");

            migrationBuilder.DropTable(
                name: "run_weapon");

            migrationBuilder.DropTable(
                name: "artifact");

            migrationBuilder.DropTable(
                name: "enemy");

            migrationBuilder.DropTable(
                name: "run");

            migrationBuilder.DropTable(
                name: "weapon");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
