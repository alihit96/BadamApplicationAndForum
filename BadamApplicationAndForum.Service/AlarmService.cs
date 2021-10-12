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
    public class AlarmService : IAlarm
    {
        private readonly ApplicationDatabaseContext _context;
        public AlarmService(ApplicationDatabaseContext context)
        {
            _context = context;
        }
        public async Task AddAlarm(Alarm alarm)
        {
            _context.Alarms.Add(alarm);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Alarm> GetAlarms()
        {
            return _context.Alarms.Include(a=>a.ApplicationUser);
        }

        public IEnumerable<Alarm> GetAlarmsById(string Id)
        {
            var user = _context.ApplicationUsers.Where(a => a.Id.ToString() == Id).FirstOrDefault();
            var alarms = _context.Alarms.Where(a => a.ApplicationUser.Id == user.Id);

            return alarms;
        }
    }
}
