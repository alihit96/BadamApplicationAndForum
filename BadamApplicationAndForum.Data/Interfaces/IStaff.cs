using BadamApplicationAndForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Interfaces
{
    public interface IStaff
    {
        IEnumerable<Staff> GetStaffs();
        Staff GetStaff(int Id);
        Task Add(Staff staff);
        Task Update(Staff staff);
        Task Delete(Staff staff);
    }
}
