namespace SocialMediaApp.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public List<int> Followers { get; set; } = new List<int>();
    }
}
