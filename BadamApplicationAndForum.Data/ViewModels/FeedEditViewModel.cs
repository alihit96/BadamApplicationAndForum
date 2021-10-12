using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace BadamApplicationAndForum.Data.ViewModels
{
    public class FeedEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "شرح")]
        public string Content { get; set; }
        [Display(Name = "تصویر")]
        public IFormFile ImageUrl { get; set; }
        [Display(Name = "نام تصویر")]
        public string ImageUrlName { get; set; }
        [Display(Name = "تاریخ")]
        public string Date { get; set; }
        [Display(Name = "گروه")]
        public string Group { get; set; }
        [Display(Name = "لینک خبر")]
        public string FeedUrl { get; set; }

    }
}
