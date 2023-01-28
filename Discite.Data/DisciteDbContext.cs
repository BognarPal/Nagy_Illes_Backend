using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discite.Data.Models;

namespace Discite.Data
{
    public class DisciteDbContext: DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = ConfigurationManager.ConnectionStrings["travels"]?.ConnectionString;

#if DEBUG
            if (string.IsNullOrWhiteSpace(connString))
                optionsBuilder.UseMySql(
                    "server=localhost;database=project_discite;uid=root;pwd=;",
                    ServerVersion.Create(10, 4, 24, ServerType.MariaDb)
                );
            else
#endif
                optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserModel>().HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<UserModel>().HasIndex(e => e.UserName).IsUnique();


            modelBuilder.Entity<ClassArtifactModel>().HasOne(e => e.Class).WithMany(e => e.Artifacts).HasForeignKey(e => e.ClassId);
            modelBuilder.Entity<ClassArtifactModel>().HasOne(e => e.Artifact).WithMany(e => e.Classes).HasForeignKey(e => e.ArtifactId);

            modelBuilder.Entity<RunArtifactModel>().HasOne(e => e.Run).WithMany(e => e.Artifacts).HasForeignKey(e => e.RunId);
            modelBuilder.Entity<RunArtifactModel>().HasOne(e => e.Artifact).WithMany(e => e.Runs).HasForeignKey(e => e.ArtifactId);

            modelBuilder.Entity<RunEnemyModel>().HasOne(e => e.Run).WithMany(e => e.Enemies).HasForeignKey(e => e.RunId);
            modelBuilder.Entity<RunEnemyModel>().HasOne(e => e.Enemy).WithMany(e => e.Runs).HasForeignKey(e => e.EnemyId);

            modelBuilder.Entity<RunWeaponModel>().HasOne(e => e.Run).WithMany(e => e.Weapons).HasForeignKey(e => e.RunId);
            modelBuilder.Entity<RunWeaponModel>().HasOne(e => e.Weapon).WithMany(e => e.Runs).HasForeignKey(e => e.WeaponId);

            modelBuilder.Entity<RunRoomModel>().HasOne(e => e.Run).WithMany(e => e.Rooms).HasForeignKey(e => e.RunId);
            modelBuilder.Entity<RunRoomModel>().HasOne(e => e.Room).WithMany(e => e.Runs).HasForeignKey(e => e.RoomId);

            modelBuilder.Entity<UserClassModel>().HasOne(e => e.User).WithMany(e => e.Classes).HasForeignKey(e => e.UserId);
            modelBuilder.Entity<UserClassModel>().HasOne(e => e.Class).WithMany(e => e.Users).HasForeignKey(e => e.ClassId);


            modelBuilder.Entity<EnemyModel>().HasData
            (
                new EnemyModel() { Id = 1, Name = "Ghoul", MaxHp = 20, Damage = 10, Energy = 0, Speed = 0.7f },
                new EnemyModel() { Id = 2, Name = "Exploder", MaxHp = 5, Damage = 25, Energy = 0, Speed = 1 },
                new EnemyModel() { Id = 3, Name = "Cyber Ghoul", MaxHp = 35, Damage = 20, Energy = 0, Speed = 1.5f },
                new EnemyModel() { Id = 4, Name = "Multi-tank", MaxHp = 60, Damage = 10, Energy = 0, Speed = 0.5f },
                new EnemyModel() { Id = 5, Name = "Agent", MaxHp = 20, Damage = 15, Energy = 0, Speed = 0.7f },
                new EnemyModel() { Id = 6, Name = "Chimera", MaxHp = 200, Damage = 15, Energy = 0, Speed = 1.2f }
            );

            modelBuilder.Entity<ClassModel>().HasData
            (
                new ClassModel() { Id = 1, Name = "Artificier", MaxHp = 70, Damage = 5, Energy = 160, Speed = 1 },
                new ClassModel() { Id = 2, Name = "Weapon Master", MaxHp = 120, Damage = 4, Energy = 100, Speed = 1 },
                new ClassModel() { Id = 3, Name = "Cyber Ninja", MaxHp = 90, Damage = 4, Energy = 130, Speed = 2 }
            );

            modelBuilder.Entity<WeaponModel>().HasData
            (
                new WeaponModel() { Id = 1, Name = "Katana", Damage = 3, AttackSpeed = 1.5f },
                new WeaponModel() { Id = 2, Name = "Spear", Damage = 4, AttackSpeed = 1 },
                new WeaponModel() { Id = 3, Name = "Deagle", Damage = 6, AttackSpeed = 0.7f },
                new WeaponModel() { Id = 4, Name = "Laser SMG", Damage = 1, AttackSpeed = 3 },
                new WeaponModel() { Id = 5, Name = "Shotgun", Damage = 1, AttackSpeed = 0.5f }
            );

            modelBuilder.Entity<ArtifactModel>().HasData
            (
                new ArtifactModel() { Id = 1, Name = "Flammable blood", MaxLevel = 3 },
                new ArtifactModel() { Id = 2, Name = "Poisonous blood", MaxLevel = 3 },
                new ArtifactModel() { Id = 3, Name = "Exploding corpses", MaxLevel = 3 },
                new ArtifactModel() { Id = 4, Name = "Revenge damage", MaxLevel = 3 }
            );

            modelBuilder.Entity<RoomModel>().HasData
            (
                new RoomModel() { Id = 1, Name = "Entry room" },
                new RoomModel() { Id = 2, Name = "Exit room" },
                new RoomModel() { Id = 3, Name = "Encounter room" },
                new RoomModel() { Id = 4, Name = "Shop room" },
                new RoomModel() { Id = 5, Name = "Boss room" }
            );
        }
    }
}
