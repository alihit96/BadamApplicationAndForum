using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Dtos.DirectMessages
{
    public class MessageIndexModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<MessageReplyModel> MessageReplies { get; set; }
    }
}
