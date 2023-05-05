using Crud_Application.Data;
using Crud_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Crud_Application.Services
{
    public class DataService : IDataService
    {
        private readonly DataContext _db;

        public DataService(DataContext db)
        {
            _db = db;
        }

        public async Task<Post> AddPost(Post post)
        {
            try
            {
                await _db.Posts.AddAsync(post);
                await _db.SaveChangesAsync();    

                return await _db.Posts.FindAsync(post.ID);
            }
            catch
            {
                return null;
            }           
        }

        public async Task<(bool, string)> DeletePost(Post post)
        {
            try
            {
                var dbPost = await _db.Posts.FindAsync(post.ID);

                if (dbPost == null)
                {
                    return (false, "Post could not be found.");
                }
                _db.Posts.Remove(post);
                await _db.SaveChangesAsync();

                return (true, "Post has been deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        public async Task<Post> GetPostByID(int id, bool includeTags = false)
        {
            try
            {
                if (includeTags)
                {
                    return await _db.Posts.Include(t => t.Tags).FirstOrDefaultAsync(i => i.ID == id);
                }
                return await _db.Posts.FirstOrDefaultAsync(i => i.ID == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Post>> GetPosts()
        {
            try
            {
                return await _db.Posts.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Post> UpdatePost(Post post)
        {
            try
            {
                _db.Entry(post).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return post;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
