using BadamApplicationAndForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Interfaces
{
    public interface IDirectMessage
    {
        IEnumerable<DirectMessage> GetDirectMessagesForAppUser(string Id);
        DirectMessage GetDirectMessage(int Id);
        Task Add(DirectMessage directMessage);
        Task Update(DirectMessage directMessage);
        Task Delete(DirectMessage directMessage);
        Task AddReply(MessageReply messageReply);
        Task EditReply(MessageReply messageReply);
        Task DeleteReply(MessageReply messageReply);

    }
}
