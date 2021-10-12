using BadamApplicationAndForum.Data.Contexts;
using BadamApplicationAndForum.Data.Interfaces;
using BadamApplicationAndForum.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Service
{
    public class DirectMessageService : IDirectMessage
    {
        private readonly ApplicationDatabaseContext _context;
        public DirectMessageService(ApplicationDatabaseContext context)
        {
            _context = context;
        }
        public async Task Add(DirectMessage directMessage)
        {
            _context.DirectMessages.Add(directMessage);
            await _context.SaveChangesAsync();
        }

        public async Task AddReply(MessageReply messageReply)
        {
            _context.MessageReplies.Add(messageReply);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(DirectMessage directMessage)
        {
            var reply = _context.DirectMessages.Where(r => r.Id == directMessage.Id).FirstOrDefault();
            reply.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReply(MessageReply messageReply)
        {
            var reply = _context.MessageReplies.Where(r=> r.Id ==  messageReply.Id).FirstOrDefault();
            reply.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task EditReply(MessageReply messageReply)
        {
            _context.MessageReplies.Update(messageReply);
            await _context.SaveChangesAsync();
        }

        public DirectMessage GetDirectMessage(int Id)
        {
            return _context.DirectMessages.Include(d=>d.MessageReplies).Include(d=>d.ApplicationUser).Include(d=>d.PanelUser).Where(d => d.Id.Equals(Id)).FirstOrDefault();
        }

        public IEnumerable<DirectMessage> GetDirectMessagesForAppUser(string Id)
        {
            return _context.DirectMessages
                .Include(d=>d.ApplicationUser)
                .Include(d=>d.MessageReplies)
                .ThenInclude(m=>m.PanelUser)
                .Where(d => d.ApplicationUser.Id.Equals(Id)).ToList();
        }

        public async Task Update(DirectMessage directMessage)
        {
            _context.DirectMessages.Update(directMessage);
            await _context.SaveChangesAsync();
        }
    }
}
