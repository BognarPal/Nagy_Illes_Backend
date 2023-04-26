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
        public DbSet<ArtifactModel> Artifacts { get; set; }
        public DbSet<WeaponModel> Weapons { get; set; }
        public DbSet<EnemyModel> Enemies { get; set; }
        public DbSet<RunModel> Runs { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RunArtifactModel> RunArtifacts { get; set; }
        public DbSet<RunEnemyModel> RunEnemies { get; set; }
        public DbSet<RunWeaponModel> RunWeaopns { get; set; }


        public static string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "server=localhost;database=project_discite;uid=root;pwd=;Convert Zero Datetime=True;",
                    ServerVersion.Create(10, 4, 24, ServerType.MariaDb)
                );
            }
#else
            optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserModel>().HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<UserModel>().HasIndex(e => e.UserName).IsUnique();

            modelBuilder.Entity<RunArtifactModel>().HasOne(e => e.Run).WithMany(e => e.Artifacts).HasForeignKey(e => e.RunId);
            modelBuilder.Entity<RunArtifactModel>().HasOne(e => e.Artifact).WithMany(e => e.Runs).HasForeignKey(e => e.ArtifactId);

            modelBuilder.Entity<RunEnemyModel>().HasOne(e => e.Run).WithMany(e => e.Enemies).HasForeignKey(e => e.RunId);
            modelBuilder.Entity<RunEnemyModel>().HasOne(e => e.Enemy).WithMany(e => e.Runs).HasForeignKey(e => e.EnemyId);

            modelBuilder.Entity<RunWeaponModel>().HasOne(e => e.Run).WithMany(e => e.Weapons).HasForeignKey(e => e.RunId);
            modelBuilder.Entity<RunWeaponModel>().HasOne(e => e.Weapon).WithMany(e => e.Runs).HasForeignKey(e => e.WeaponId);


            modelBuilder.Entity<EnemyModel>().HasData
            (
                new EnemyModel() { Id = 1, Name = "Ghoul", Health = 20, Damage = 10, Speed = 0.7f },
                new EnemyModel() { Id = 2, Name = "Exploder", Health = 5, Damage = 25, Speed = 1 },
                new EnemyModel() { Id = 3, Name = "Cyber Ghoul", Health = 35, Damage = 20, Speed = 1.5f },
                new EnemyModel() { Id = 4, Name = "Multi-tank", Health = 60, Damage = 10, Speed = 0.5f },
                new EnemyModel() { Id = 5, Name = "Agent", Health = 20, Damage = 15, Speed = 0.7f },
                new EnemyModel() { Id = 6, Name = "Chimera", Health = 200, Damage = 15, Speed = 1.2f }
            );

            modelBuilder.Entity<WeaponModel>().HasData
            (
                new WeaponModel() { Id = 1, Name = "Katana", Damage = 3, Speed = 1.5f },
                new WeaponModel() { Id = 2, Name = "Spear", Damage = 4, Speed = 1 },
                new WeaponModel() { Id = 3, Name = "Deagle", Damage = 6, Speed = 0.7f },
                new WeaponModel() { Id = 4, Name = "Laser SMG", Damage = 1, Speed = 3 },
                new WeaponModel() { Id = 5, Name = "Shotgun", Damage = 1, Speed = 0.5f }
            );

            modelBuilder.Entity<ArtifactModel>().HasData
            (
                new ArtifactModel() { Id = 1, Name = "Flammable blood", Power = 3 },
                new ArtifactModel() { Id = 2, Name = "Poisonous blood", Power = 3 },
                new ArtifactModel() { Id = 3, Name = "Exploding corpses", Power = 3 },
                new ArtifactModel() { Id = 4, Name = "Revenge damage", Power = 3 }
            );
        }
    }
}
