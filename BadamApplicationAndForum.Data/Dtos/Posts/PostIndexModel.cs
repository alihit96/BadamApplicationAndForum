using BadamApplicationAndForum.Data.Dtos.Replies;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadamApplicationAndForum.Data.Dtos.Posts
{
    public class PostIndexModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<PostReplyModel> Replies { get; set; }
    }
}
