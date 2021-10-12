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
    public class NewsService : INews
    {
        private readonly ApplicationDatabaseContext _context;
        public NewsService(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddFeed(News news)
        {
            _context.News.Add(news);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFeed(News news)
        {
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<News> GetAllNews()
        {
            return _context.News.OrderByDescending(n=> n.Date).ToList();
        }

        public News GetFeed(int Id)
        {
            return _context.News.Where(n => n.Id == Id).FirstOrDefault();
        }

        public int NewsCount()
        {
            return _context.News.ToList().Count;
        }

        public async Task UpdateFeed(News news)
        {
            _context.News.Update(news);
            await _context.SaveChangesAsync();
        }
    }
}
