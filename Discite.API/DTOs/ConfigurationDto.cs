using Discite.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discite.API.DTOs
{
    public class Weapon
    {
        public Weapon() { }
        public Weapon(WeaponModel weapon)
        {
            Id = weapon.Id;
            Name = weapon.Name;
            Damage = weapon.Damage;
            Speed = weapon.Speed;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public float Damage { get; set; }
        public float Speed { get; set; }
    }
    public class Enemy
    {
        public Enemy() { }
        public Enemy(EnemyModel enemy)
        {
            Id = enemy.Id;
            Name = enemy.Name;
            Health = enemy.Health;
            Damage = enemy.Damage;
            Speed = enemy.Speed;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public float Health { get; set; }
        public float Damage { get; set; }
        public float Speed { get; set; }
    }
    public class Artifact
    {
        public Artifact() { }
        public Artifact(ArtifactModel artifact)
        {
            Id = artifact.Id;
            Name = artifact.Name;
            Power = artifact.Power;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public float Power { get; set; }
    }
    public class ConfigurationDto
    {
        public IEnumerable<Weapon> Weapons { get; set; }
        public IEnumerable<Enemy> Enemies { get; set; }
        public IEnumerable<Artifact> Artifacts { get; set; }
    }
}
