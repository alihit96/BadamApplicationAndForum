using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.ViewModels
{
    public class MessagesListingViewModel
    {
        public IEnumerable<UserDirectMessagesViewModel> UserDirectMessagesViewModels { get; set; }
        public IEnumerable<MessageReplyViewModel> MessageReplyViewModels { get; set; }
        public IEnumerable<UserDirectMessagesViewModel> UserDeletedDirectMessagesViewModels { get; set; }
    }
}
