using BadamApplicationAndForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Interfaces
{
    public interface IPost
    {
        Post GetPostById(int id);
        IEnumerable<Post> GetAllPosts();
        IEnumerable<Post> GetFilteredPosts(string searchQuery);
        IEnumerable<Post> GetPostsByForum(int forumId);

        Task Add(Post post);
        Task Delete(int id);
    }
}
