using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.ViewModels
{
    public class AlarmCreateViewModel
    {
        [Display(Name ="عنوان")]
        public string Title { get; set; }
        [Display(Name = "شرح")]
        public string Content { get; set; }
        [Display(Name = "دریافت کننده")]
        public string UserName { get; set; }
        public IEnumerable<SelectListItem> UserNames { get; set; }
        [Display(Name = "تاریخ")]
        public string Date { get; set; }

    }
}
