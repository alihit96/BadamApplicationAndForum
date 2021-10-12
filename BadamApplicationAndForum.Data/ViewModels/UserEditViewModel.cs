using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.ViewModels
{
    public class UserEditViewModel
    {
        public string Id { get; set; }
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }
        [Display(Name = "شماره تلفن")]
        public string MobileNumber { get; set; }
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
    }
}
