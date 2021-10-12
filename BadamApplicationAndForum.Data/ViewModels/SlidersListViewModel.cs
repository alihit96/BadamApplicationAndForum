using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.ViewModels
{
    public class SlidersListViewModel
    {
        public int Id { get; set; }
        [Display(Name= "تصویر")]
        public string ImageUrl { get; set; }
        [Display(Name = "سر عنوان")]
        public string Title { get; set; }
    }
}
