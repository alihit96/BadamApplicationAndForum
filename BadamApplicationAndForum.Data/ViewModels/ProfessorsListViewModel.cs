using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.ViewModels
{
    public class ProfessorsListViewModel
    {
        public int Id { get; set; }
        [Display(Name="نام و نام خانوادگی")]
        public string Name { get; set; }
        [Display(Name = "تصویر")]
        public string ImageUrl { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        public string Group { get; set; }
    }
}
