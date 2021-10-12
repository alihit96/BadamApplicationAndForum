using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Models
{
    public class DirectMessage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual IEnumerable<MessageReply> MessageReplies { get; set; }
        public virtual PanelUser PanelUser { get; set; }
    }
}
