using BadamApplicationAndForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Interfaces
{
    public interface INews
    {
        IEnumerable<News> GetAllNews();
        News GetFeed(int Id);
        int NewsCount();
        Task AddFeed(News news);
        Task UpdateFeed(News news);
        Task DeleteFeed(News news);
    }
}
