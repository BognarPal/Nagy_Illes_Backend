namespace Discite.API.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastActive { get; set; }
        public IEnumerable<int> Runs { get; set; }
        public IEnumerable<int> Classes { get; set; }
    }
}
