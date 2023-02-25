namespace Discite.API.DTOs
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Score { get; set; }
    }
    public class ToplistDto
    {
        public User[] Users { get; set; }
    }
}
