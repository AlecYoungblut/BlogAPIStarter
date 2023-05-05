namespace Crud_Application.Models
{
    public class Post
    {
        public int ID { get; set; }        
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Date_Created { get; set; }
        public DateTime Date_Edited { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
        public ICollection<Tag> Tags { get; }
    }
}
