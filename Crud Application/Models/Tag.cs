namespace Crud_Application.Models
{
    public class Tag
    {
        public int ID { get; set; }        
        public string TagName { get; set; } = string.Empty;
        public DateTime TagAdded { get; set; }

        public int PostID { get; set; }
        public Post Post { get; set; }
    }
}
