using BadamApplicationAndForum.Data.Contexts;
using BadamApplicationAndForum.Data.Interfaces;
using BadamApplicationAndForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Service
{
    public class ReplyService : IReply
    {
        private readonly ApplicationDatabaseContext _context;
        public ReplyService(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        public async Task Add(PostReply reply)
        {
            _context.PostReplies.Add(reply);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PostReply> GetPostRepliesByPost(int id)
        {
            return _context.Posts.Where(p => p.Id == id).First().PostReplies;
        }
    }
}
