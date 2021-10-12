using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BadamApplicationAndForum.Data.Dtos.News
{
    public class NewsListingModel
    {
        public int Id { get; set; }
        [Display(Name="عنوان")]
        public string Title { get; set; }
        [Display(Name = "شرح")]
        public string Content { get; set; }
        [Display(Name = "تصویر")]
        public string ImageUrl { get; set; }
        [Display(Name = "تاریخ")]
        public string Date { get; set; }
        [Display(Name = "گروه")]
        public string Group { get; set; }
        public string FeedUrl { get; set; }
    }
}
