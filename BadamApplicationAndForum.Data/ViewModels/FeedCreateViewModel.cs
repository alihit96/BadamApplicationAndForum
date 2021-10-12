using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.ViewModels
{
    public class FeedCreateViewModel
    {
        [Display(Name ="عنوان")]
        [Required(ErrorMessage ="نباید بدون مقدار باشد")]
        public string Title { get; set; }
        [Display(Name = "شرح")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public string Content { get; set; }
        [Display(Name = "تصویر")]
        public IFormFile ImageUrl { get; set; }
        [Display(Name = "تاریخ")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public string Date { get; set; }
        [Display(Name = "گروه")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public string Group { get; set; }
        [Display(Name = "ارسال به")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public string ReceivingGroup { get; set; }
        [Display(Name = "لینک خبر")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public string FeedUrl { get; set; }

    }
}
