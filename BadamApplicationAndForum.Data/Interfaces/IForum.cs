using BadamApplicationAndForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Interfaces
{
    public interface IForum
    {
        Forum GetForumById(int id);
        IEnumerable<Forum> GetAllForums();
        IEnumerable<ApplicationUser> GetActiveApplicationUsers();

        Task Create(Forum forum);
        Task Delete(int forumId);
    }
}
