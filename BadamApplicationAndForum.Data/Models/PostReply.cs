using System;
using System.Collections.Generic;
using System.Text;

namespace BadamApplicationAndForum.Data.Models
{
    public class PostReply
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Post Post { get; set; }
    }
}
