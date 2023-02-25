using System.ComponentModel.DataAnnotations.Schema;

namespace Discite.API.DTOs
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Damage { get; set; }
        public float AttackSpeed { get; set; }
    }
    public class Enemy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float MaxHp { get; set; }
        public float Damage { get; set; }
        public float Energy { get; set; }
        public float Speed { get; set; }
    }
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float MaxHp { get; set; }
        public float Damage { get; set; }
        public float Energy { get; set; }
        public float Speed { get; set; }
    }
    public class Artifact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxLevel { get; set; }
    }
    public class ConfigurationDto
    {
        public IEnumerable<Weapon> Weapons { get; set; }
        public IEnumerable<Enemy> Enemies { get; set; }
        public IEnumerable<Class> Classes { get; set; }
        public IEnumerable<Artifact> Artifacts { get; set; }
    }
}
