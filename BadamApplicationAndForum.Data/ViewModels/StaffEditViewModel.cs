using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BadamApplicationAndForum.Data.ViewModels
{
    public class StaffEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "مقدار اجباری")]
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "مقدار اجباری")]
        [Display(Name = "مسئولیت")]
        public string Duty { get; set; }
        [Required(ErrorMessage = "مقدار اجباری")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Required(ErrorMessage = "مقدار اجباری")]
        [Display(Name = "شماره تلفن")]
        public string Phone { get; set; }
        [Display(Name = "تصویر")]
        public IFormFile ImageUrl { get; set; }
        [Display(Name = "وظایف")]
        public string Duties { get; set; }
        [Required(ErrorMessage = "مقدار اجباری")]
        [Display(Name = "نام استاندارد")]
        public string NormalName { get; set; }
        [Display(Name ="معاونت")]
        public string Department { get; set; }
    }
}
