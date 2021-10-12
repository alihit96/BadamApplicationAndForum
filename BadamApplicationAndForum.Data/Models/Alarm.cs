using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Models
{
    public class Alarm
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Date { get; set; }

    }
}
