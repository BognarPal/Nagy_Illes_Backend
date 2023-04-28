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
                new EnemyModel() { Id = 1, Name = "Baby Bat", Health = 20, Damage = 10, Speed = 0.7f },
                new EnemyModel() { Id = 2, Name = "Juvenile Bat", Health = 5, Damage = 25, Speed = 1 },
                new EnemyModel() { Id = 3, Name = "Adult Bat", Health = 35, Damage = 20, Speed = 1.5f },
                new EnemyModel() { Id = 4, Name = "Bat Queen", Health = 60, Damage = 10, Speed = 0.5f }
            );

            modelBuilder.Entity<WeaponModel>().HasData
            (
                new WeaponModel() { Id = 1, Name = "Assault Rifle", Damage = 3, Speed = 1.5f },
                new WeaponModel() { Id = 2, Name = "Anti Matter Rifle", Damage = 4, Speed = 1 },
                new WeaponModel() { Id = 3, Name = "Plasma Pistol", Damage = 6, Speed = 0.7f },
                new WeaponModel() { Id = 4, Name = "Singularity Blaster", Damage = 1, Speed = 3 }
            );

            modelBuilder.Entity<ArtifactModel>().HasData
            (
                new ArtifactModel() { Id = 1, Name = "Damage Up", Power = 3 },
                new ArtifactModel() { Id = 2, Name = "Attack Speed Up", Power = 3 },
                new ArtifactModel() { Id = 3, Name = "Explosion Radius Up", Power = 3 },
                new ArtifactModel() { Id = 4, Name = "Movement Speed Up", Power = 3 },
                new ArtifactModel() { Id = 5, Name = "Double Shot", Power = 3 },
                new ArtifactModel() { Id = 6, Name = "Max Health Up", Power = 3 }
            );
        }
    }
}
