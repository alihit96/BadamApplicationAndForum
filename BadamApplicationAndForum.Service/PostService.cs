using BadamApplicationAndForum.Data.Contexts;
using BadamApplicationAndForum.Data.Interfaces;
using BadamApplicationAndForum.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Service
{
    public class PostService : IPost
    {
        private readonly ApplicationDatabaseContext _context;

        public PostService(ApplicationDatabaseContext context)
        {
            _context = context;
        }
        public async Task Add(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var post = _context.Posts.Where(p => p.Id == id)
                .Include(p => p.ApplicationUser)
                .Include(p => p.PostReplies)
                    .ThenInclude(r => r.ApplicationUser)
                .Include(p => p.Forum).First();
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            throw new NotImplementedException();
        }

        public Post GetPostById(int id)
        {
            return _context.Posts.Where(p => p.Id == id)
                .Include(p => p.ApplicationUser)
                .Include(p => p.PostReplies)
                    .ThenInclude(r => r.ApplicationUser)
                .Include(p => p.Forum).First();
        }

        public IEnumerable<Post> GetPostsByForum(int forumId)
        {
            var forum = _context.Forums
                .Where(f => f.Id == forumId)
                .First();
            return forum.Posts;
        }
    }
}
