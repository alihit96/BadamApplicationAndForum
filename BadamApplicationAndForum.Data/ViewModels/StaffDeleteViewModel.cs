using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BadamApplicationAndForum.Data.ViewModels
{
    public class StaffDeleteViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }
        [Display(Name = "مسئولیت")]
        public string Duty { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "شماره تلفن")]
        public string Phone { get; set; }
        [Display(Name = "تصویر")]
        public string ImageUrl { get; set; }
        [Display(Name = "وظایف")]
        public string Duties { get; set; }
        [Display(Name = "نام استاندارد")]
        public string NormalName { get; set; }
        [Display(Name = "معاونت")]
        public string Department { get; set; }
    }
}
