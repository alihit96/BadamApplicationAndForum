using BadamApplicationAndForum.Data.Dtos.Forums;
using BadamApplicationAndForum.Data.Dtos.Replies;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadamApplicationAndForum.Data.Dtos.Posts
{
    public class PostListingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public string AuthorId { get; set; }
        public DateTime DatePosted { get; set; }
        public ForumListingModel Forum { get; set; }
        public int RepliesCount { get; set; }
        public IEnumerable<PostReplyModel> Replies { get; set; }
    }
}
