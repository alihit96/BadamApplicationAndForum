using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.ViewModels
{
    public class AlarmsListViewModel
    {
        [Display(Name ="شناسه")]
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "متن")]
        public string Content { get; set; }
        [Display(Name = "کاربر")]
        public string ApplicationUser { get; set; }
        [Display(Name = "تاریخ")]
        public string Date { get; set; }
    }
}
