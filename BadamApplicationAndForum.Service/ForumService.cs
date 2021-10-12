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
    public class ForumService : IForum
    {
        private readonly ApplicationDatabaseContext _context;

        public ForumService(ApplicationDatabaseContext context)
        {
            _context = context;
        }
        public Task Create(Forum forum)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int forumId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetActiveApplicationUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Forum> GetAllForums()
        {
            return _context.Forums.Include(f => f.Posts);
        }

        public Forum GetForumById(int id)
        {
            return _context.Forums.Where(f => f.Id == id)
                .Include(f => f.Posts)
                    .ThenInclude(p => p.ApplicationUser)
                .Include(f => f.Posts)
                    .ThenInclude(p => p.PostReplies)
                        .ThenInclude(r => r.ApplicationUser)
                .FirstOrDefault();
        }
    }
}
