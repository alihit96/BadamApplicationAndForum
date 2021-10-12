using System.ComponentModel.DataAnnotations;

namespace BadamApplicationAndForum.Data.ViewModels
{
    public class UsersListViewModel
    {
        public string Id { get; set; }
        [Display(Name ="نام و نام خانوادگی")]
        public string FullName { get; set; }
        [Display(Name ="نام کاربری")]
        public string UserName { get; set; }
        [Display(Name ="شماره تلفن همراه")]
        public string MobileNumber { get; set; }
    }
}
