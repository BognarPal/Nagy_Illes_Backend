using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Discite.Data.Migrations
{
    public partial class ManyToMany : Migration
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
                    register_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
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
                    AttackSpeed = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weapon", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "class_artifact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    ArtifactId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_class_artifact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_class_artifact_artifact_ArtifactId",
                        column: x => x.ArtifactId,
                        principalTable: "artifact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_class_artifact_class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "run",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: true),
                    Path = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gold = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Runtime = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    version = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CurrentHp = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_run", x => x.Id);
                    table.ForeignKey(
                        name: "FK_run_class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "class",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_run_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_class",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_class", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_class_class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_class_user_UserId",
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
                    Picked = table.Column<int>(type: "int", nullable: false),
                    Seen = table.Column<int>(type: "int", nullable: false),
                    Used = table.Column<int>(type: "int", nullable: false)
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
                name: "run_room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RunId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Seen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_run_room", x => x.Id);
                    table.ForeignKey(
                        name: "FK_run_room_eventroom_RoomId",
                        column: x => x.RoomId,
                        principalTable: "eventroom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_run_room_run_RunId",
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
                    Picked = table.Column<int>(type: "int", nullable: false),
                    Seen = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_class_artifact_ArtifactId",
                table: "class_artifact",
                column: "ArtifactId");

            migrationBuilder.CreateIndex(
                name: "IX_class_artifact_ClassId",
                table: "class_artifact",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_run_ClassId",
                table: "run",
                column: "ClassId");

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
                name: "IX_run_room_RoomId",
                table: "run_room",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_run_room_RunId",
                table: "run_room",
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

            migrationBuilder.CreateIndex(
                name: "IX_user_class_ClassId",
                table: "user_class",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_user_class_UserId",
                table: "user_class",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "class_artifact");

            migrationBuilder.DropTable(
                name: "run_artifact");

            migrationBuilder.DropTable(
                name: "run_enemy");

            migrationBuilder.DropTable(
                name: "run_room");

            migrationBuilder.DropTable(
                name: "run_weapon");

            migrationBuilder.DropTable(
                name: "user_class");

            migrationBuilder.DropTable(
                name: "artifact");

            migrationBuilder.DropTable(
                name: "enemy");

            migrationBuilder.DropTable(
                name: "eventroom");

            migrationBuilder.DropTable(
                name: "run");

            migrationBuilder.DropTable(
                name: "weapon");

            migrationBuilder.DropTable(
                name: "class");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
