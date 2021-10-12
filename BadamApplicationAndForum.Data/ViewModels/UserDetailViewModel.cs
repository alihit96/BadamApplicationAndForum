
using System.ComponentModel.DataAnnotations;


namespace BadamApplicationAndForum.Data.ViewModels
{
    public class UserDetailViewModel
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
