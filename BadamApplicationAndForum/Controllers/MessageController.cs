using BadamApplicationAndForum.Data.Dtos;
using BadamApplicationAndForum.Data.Dtos.DirectMessages;
using BadamApplicationAndForum.Data.Interfaces;
using BadamApplicationAndForum.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BadamApplicationAndForum.Controllers
{
    [ApiController]
    [Route("api/app/messages")]
    public class MessageController : Controller
    {
        private readonly IDirectMessage _message;
        private readonly IApplicationUser _user;
        private readonly UserManager<PanelUser> _userManager;
        public MessageController(IDirectMessage message, IApplicationUser user, UserManager<PanelUser> userManager)
        {
            _message = message;
            _user = user;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = MVSJwtTokens.AuthScheme)]
        public ActionResult GetMessages(string Id)
        {
            var user = _user.FindUserById(Id);
            var messages = user.DirectMessages;
            var model = messages.Select(m => new DirectMessagesDto()
            {
                Id = m.Id,
                Title = m.Title,
                ApplicationUserId = m.ApplicationUser.Id.ToString(),
                ApplicationUserName = m.ApplicationUser.FullName,
                Content = m.Content,
                Created = m.Created,
                IsDeleted = m.IsDeleted,
                IsRead = m.IsRead,
                PanelUserId = m.PanelUser.Id,
                PanelUserName = m.PanelUser.UserName,
                MessageReplies = BuildMessageReplies(m.MessageReplies)
            });
            return Ok(new 
            {
                Messages = model
            });
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = MVSJwtTokens.AuthScheme)]
        public ActionResult AddMessage(NewDirectMessageDto directMessageDto)
        {
            var user = _user.FindUserById(directMessageDto.ApplicationUserId);
            var panelUser = _userManager.FindByNameAsync(directMessageDto.PanelUserName).Result;
            if (panelUser == null)
            {
                return Ok(new { message = "امکان دریافت پیام فعلا برای ایشان تعریف نشده است."});
            }
            DirectMessage directMessage = new DirectMessage()
            {
                Title = directMessageDto.Title,
                Content = directMessageDto.Content,
                Created = DateTime.Now,
                IsDeleted = false,
                IsRead = false,
                ApplicationUser = user,
                PanelUser = panelUser,
            };
            _message.Add(directMessage).Wait();
            return Ok(new { messageId = directMessage.Id });
        }


        private IEnumerable<MessageReplyModel> BuildMessageReplies(IEnumerable<MessageReply> messageReplies)
        {
            return messageReplies.Select(reply => new MessageReplyModel
            {
                Id = reply.Id,
                Reply = reply.Reply,
                PanelUserId = reply.PanelUser.Id,
                PanelUserName = reply.PanelUser.UserName,
                IsDeleted = reply.IsDeleted,
                Created = reply.Created
            });

        }
    }
}
