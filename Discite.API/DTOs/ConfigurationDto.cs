using System.ComponentModel.DataAnnotations.Schema;

namespace Discite.API.DTOs
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Damage { get; set; }
        public float Speed { get; set; }
    }
    public class Enemy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Health { get; set; }
        public float Damage { get; set; }
        public float Speed { get; set; }
    }
    public class Artifact
    {
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
