using BlogApp.Data;
using BlogApp.Interfaces;
using BlogApp.Models;
using BlogApp.Models.Comments;
using BlogApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Repository
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository( ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public IndexViewModel GetAllPosts(int pageNumber, string category)
        {
            Func<Post, bool> InCategory = (Post) =>
            {
                return Post.Category.ToLower().Equals(category.ToLower());
            };
            int pageSize = 5;
            int skipAmount = pageSize * (pageNumber - 1);

            var query = _context.Posts.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(x => InCategory(x));
            }

            int postCount = query.Count();

            return new IndexViewModel
            {
                PageNumber = pageNumber,
                NextPage = postCount > skipAmount + pageNumber,
                Category = category,
                Posts = query
                .Skip(skipAmount)
                .Take(pageSize)
                .ToList()

            };
        }

        public Post GetPost(int id)
        {
            return _context.Posts.Include(p => p.MainComments).ThenInclude(m => m.SubComments).FirstOrDefault(p => p.Id == id);
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
        }

        public void RemovePost(int id)
        {
            _context.Posts.Remove(GetPost(id));
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public void AddSubComment(SubComment comment)
        {
            _context.SubComments.Add(comment);
        }
    }
}
