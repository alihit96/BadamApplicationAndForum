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
    public class RegulationService : IRegulation
    {
        private readonly ApplicationDatabaseContext _context;
        public RegulationService(ApplicationDatabaseContext context)
        {
            _context = context;
        }
        public async Task Add(Regulation regulation)
        {
            _context.Regulations.Add(regulation);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Regulation regulation)
        {
            _context.Regulations.Remove(regulation);
            await _context.SaveChangesAsync();
        }

        public Regulation GetRegulation(int Id)
        {
            return _context.Regulations.Where(r=> r.Id.Equals(Id)).FirstOrDefault();
        }

        public IEnumerable<Regulation> GetRegulations()
        {
            return _context.Regulations;
        }

        public async Task Update(Regulation regulation)
        {
            _context.Regulations.Update(regulation);
            await _context.SaveChangesAsync();
        }
    }
}
