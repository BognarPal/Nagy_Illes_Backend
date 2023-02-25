namespace Discite.API.DTOs
{
    public class WeaponStat 
    {
        public int Id { get; set; }
        public int Kills { get; set; }
        public int Shoots { get; set; }
        public int Picked { get; set; }
    } 
    public class WeaponStatsDto
    {
        public IEnumerable<WeaponStat> WeaponStats { get; set; }
    }
}
