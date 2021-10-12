using BadamApplicationAndForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Dtos.DirectMessages
{
    public class DirectMessagesDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        public string ApplicationUserName { get; set; }
        public string ApplicationUserId { get; set; }
        public IEnumerable<MessageReplyModel> MessageReplies { get; set; }
        public string PanelUserName { get; set; }
        public string PanelUserId { get; set; }
    }
}
