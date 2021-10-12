using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.ViewModels
{
    public class MessageRepliesListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public string AppUserName { get; set; }
        public string AppUserEmail { get; set; }
        public string NewReply { get; set; }
        public IEnumerable<MessageRepliesModel> RepliesModel { get; set; }
    }
}
