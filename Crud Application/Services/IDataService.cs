using Crud_Application.Models;

namespace Crud_Application.Services
{
    public interface IDataService
    {
        // Posts Services
        Task<List<Post>> GetPosts();
        Task<Post> GetPostByID(int id, bool includeTags = false);
        Task<Post> AddPost(Post post);
        Task<Post> UpdatePost(Post post);
        Task<(bool, string)> DeletePost(Post post);      
    }
}
