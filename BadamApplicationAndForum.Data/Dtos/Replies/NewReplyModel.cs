using System;
using System.Collections.Generic;
using System.Text;

namespace BadamApplicationAndForum.Data.Dtos.Replies
{
    public class NewReplyModel
    {
        public int PostId { get; set; }
        public string PostName { get; set; }
        public string AuthorName { get; set; }
        public string AuthorId { get; set; }
        public string Content { get; set; }
    }
}
