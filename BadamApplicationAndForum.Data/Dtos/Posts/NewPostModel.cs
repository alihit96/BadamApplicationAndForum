using System;
using System.Collections.Generic;
using System.Text;

namespace BadamApplicationAndForum.Data.Dtos.Posts
{
    public class NewPostModel
    {
        public int ForumId { get; set; }
        public string ForumName { get; set; }
        public string AuthorName { get; set; }
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
