namespace Discite.API.DTOs
{
    public class EnemyStat
    {
        public int Id { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
    }
    public class EnemyStatsDto
    {
        public IEnumerable<EnemyStat> EnemyStats { get; set; }
    }
}
