namespace Discite.API.DTOs
{
    public class ClassStat
    {
        public int Id { get; set; }
        public int Owned { get; set; }
        public int Used { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
    }
    public class ClassStatsDto
    {
        public ClassStat[] ClassStats { get; set; }
    }
}
