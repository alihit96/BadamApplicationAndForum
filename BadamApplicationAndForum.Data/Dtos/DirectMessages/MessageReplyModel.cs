using BadamApplicationAndForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Dtos.DirectMessages
{
    public class MessageReplyModel
    {
        public int Id { get; set; }
        public string Reply { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public string PanelUserId { get; set; }
        public string PanelUserName { get; set; }
    }
}
