using System;
using System.Collections.Generic;
using System.Text;

namespace BadamApplicationAndForum.Data.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string Date { get; set; }
        public string Group { get; set; }
        public string ReceivingGroup { get; set; }
        public string FeedUrl { get; set; }
    }
}
