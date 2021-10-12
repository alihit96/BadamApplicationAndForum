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
    public class StaffService : IStaff
    {
        private readonly ApplicationDatabaseContext _context;
        public StaffService(ApplicationDatabaseContext context)
        {
            _context = context;
        }
        public async Task Add(Staff staff)
        {
            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Staff staff)
        {
            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();
        }

        public Staff GetStaff(int Id)
        {
            return _context.Staffs.Where(s => s.Id.Equals(Id)).FirstOrDefault();
        }

        public IEnumerable<Staff> GetStaffs()
        {
            return _context.Staffs;
        }

        public async Task Update(Staff staff)
        {
            _context.Staffs.Update(staff);
            await _context.SaveChangesAsync();
        }
    }
}
