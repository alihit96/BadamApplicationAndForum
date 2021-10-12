using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Dtos.Professors
{
    public class StaffListingModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Duty { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; } = "";
        public string Duties { get; set; } = "";
        public string NormalName { get; set; }
        public string Department { get; set; }
    }
}
