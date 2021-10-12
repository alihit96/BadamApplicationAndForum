
using BadamApplicationAndForum.Data.Contexts;
using BadamApplicationAndForum.Data.Interfaces;
using ExamCards.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Service
{
    public class SaveLogService : ISaveLog
    {
        private readonly ApplicationDatabaseContext _context;
        public SaveLogService(ApplicationDatabaseContext context)
        {
            _context = context;
        }
        public async Task AddLog(SystemLog log)
        {
            _context.SystemLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<SystemLog> GetSystemLogs()
        {
            return _context.SystemLogs.OrderByDescending(l=> l.CreatedAt);
        }
    }
}
