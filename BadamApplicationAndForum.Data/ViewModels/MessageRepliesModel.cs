using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.ViewModels
{
    public class MessageRepliesModel
    {
        public int Id { get; set; }
        public string Reply { get; set; }
        public DateTime Created { get; set; }
    }
}
