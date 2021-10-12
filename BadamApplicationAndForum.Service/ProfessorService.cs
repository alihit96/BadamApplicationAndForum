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
    public class ProfessorService : IProfessor
    {
        private readonly ApplicationDatabaseContext _context;
        public ProfessorService(ApplicationDatabaseContext context)
        {
            _context = context;
        }
        public async Task AddProfessor(Professor professor)
        {
            _context.Professors.Add(professor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProfessor(int id)
        {
            var professor = _context.Professors.Where(p => p.Id == id);
            _context.Remove(professor);
            await _context.SaveChangesAsync();
        }

        public Professor GetProfessor(int id)
        {
            return _context.Professors.Where(p => p.Id == id).FirstOrDefault();
        }

        public IEnumerable<Professor> GetProfessors()
        {
            return _context.Professors;
        }

        public async Task UpdateProfessor(Professor professor)
        {
            _context.Professors.Update(professor);
            await _context.SaveChangesAsync();
        }
    }
}
