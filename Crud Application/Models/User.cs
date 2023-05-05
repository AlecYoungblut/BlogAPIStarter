namespace Crud_Application.Models
{
    public class User
    {
        public int ID { get; set; }
        public string First_Name { get; set; } = string.Empty;
        public string Last_Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;

        public ICollection<Post> Posts { get; }
    }
}
