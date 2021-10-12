using BadamApplicationAndForum.Data.Dtos;
using BadamApplicationAndForum.Data.Dtos.DirectMessages;
using BadamApplicationAndForum.Data.Interfaces;
using BadamApplicationAndForum.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Controllers
{
    [ApiController]
    [Route("api/app/message")]
    public class MessageRplyController : Controller
    {
        private readonly IDirectMessage _message;
        public MessageRplyController(IDirectMessage directMessage)
        {
            _message = directMessage;
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = MVSJwtTokens.AuthScheme)]
        public ActionResult GetMessage(int Id)
        {
            var message = _message.GetDirectMessage(Id);
            if (message == null)
            {
                return NotFound();
            }
            var replies = BuildMessageReplies(message.MessageReplies);
            var model = new MessageIndexModel
            {
                Id = message.Id,
                Content = message.Content,
                Title = message.Title,
                Created = message.Created,
                MessageReplies = replies
            };
            return Ok(model);
        }

        private IEnumerable<MessageReplyModel> BuildMessageReplies(IEnumerable<MessageReply> messageReplies)
        {
            return messageReplies.Select(reply => new MessageReplyModel
            {
                Id = reply.Id,
                Reply = reply.Reply,
                Created = reply.Created,
                PanelUserId = reply.PanelUser.Id,
                PanelUserName = reply.PanelUser.UserName
            });
        }
    }
}
