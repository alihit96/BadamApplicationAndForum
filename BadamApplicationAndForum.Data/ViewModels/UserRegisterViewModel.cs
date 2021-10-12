using System.ComponentModel.DataAnnotations;

namespace BadamApplicationAndForum.Data.ViewModels
{
    public class UserRegisterViewModel
    {
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage ="نباید بدون مقدار باشد")]
        public string FullName { get; set; }
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage ="نباید بدون مقدار باشد")]
        public string MobileNumber { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public string UserName { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public string Password { get; set; }
    }
}
