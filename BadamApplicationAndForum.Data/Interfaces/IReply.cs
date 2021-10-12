using BadamApplicationAndForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Interfaces
{
    public interface IReply
    {
        IEnumerable<PostReply> GetPostRepliesByPost(int id);
        Task Add(PostReply reply);
    }
}
