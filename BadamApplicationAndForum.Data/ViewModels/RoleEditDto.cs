using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BadamApplicationAndForum.Data.ViewModels
{
    public class RoleEditDto
    {
        public string Id { get; set; }
        [Display(Name = "نام نقش")]
        public string Name { get; set; }
    }
}
