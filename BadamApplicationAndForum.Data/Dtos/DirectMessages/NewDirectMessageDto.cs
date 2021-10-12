using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Dtos.DirectMessages
{
    public class NewDirectMessageDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ApplicationUserId { get; set; }
        public string PanelUserName { get; set; }
    }
}
