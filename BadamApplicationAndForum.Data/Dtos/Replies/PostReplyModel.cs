using System;
using System.Collections.Generic;
using System.Text;

namespace BadamApplicationAndForum.Data.Dtos.Replies
{
    public class PostReplyModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime Created { get; set; }

        public int PostId { get; set; }

    }
}
