using BadamApplicationAndForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Interfaces
{
    public interface IProfessor
    {
        IEnumerable<Professor> GetProfessors();
        Professor GetProfessor(int id);
        Task AddProfessor(Professor professor);
        Task DeleteProfessor(int id);
        Task UpdateProfessor(Professor professor);

    }
}
