using BadamApplicationAndForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Interfaces
{
    public interface IRegulation
    {
        IEnumerable<Regulation> GetRegulations();
        Regulation GetRegulation(int Id);
        Task Add(Regulation regulation);
        Task Update(Regulation regulation);
        Task Delete(Regulation regulation);
    }
}
