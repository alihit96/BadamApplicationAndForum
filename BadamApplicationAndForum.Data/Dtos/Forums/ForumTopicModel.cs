using BadamApplicationAndForum.Data.Dtos.Posts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadamApplicationAndForum.Data.Dtos.Forums
{
    public class ForumTopicModel
    {
        public ForumListingModel Forum { get; set; }
        public IEnumerable<PostListingModel> Posts { get; set; }
    }
}
