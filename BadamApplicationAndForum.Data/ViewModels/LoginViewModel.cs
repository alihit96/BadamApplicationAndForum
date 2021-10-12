using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "کلمه عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "مرا به خاطر بسپار")]
        public bool IsPersistent { get; set; }
    }
}
