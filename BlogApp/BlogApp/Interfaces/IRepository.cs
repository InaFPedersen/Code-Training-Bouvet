

using BlogApp.Models;
using BlogApp.Models.Comments;
using BlogApp.ViewModels;

namespace BlogApp.Interfaces
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();

        IndexViewModel GetAllPosts(int pageNumber, string category);

        void AddPost(Post post);

        void UpdatePost(Post post);

        void RemovePost(int id);

        Task<bool> SaveChangesAsync();

        void AddSubComment(SubComment comment);
    }
}
