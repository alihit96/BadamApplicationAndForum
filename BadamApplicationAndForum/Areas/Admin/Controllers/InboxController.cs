using BadamApplicationAndForum.Data.Contexts;
using BadamApplicationAndForum.Data.Interfaces;
using BadamApplicationAndForum.Data.Models;
using BadamApplicationAndForum.Data.ViewModels;
using BadamApplicationAndForum.Helpers;
using ExamCards.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InboxController : Controller
    {
        CurrentPersianDate persianDate = new CurrentPersianDate();
        private readonly UserManager<PanelUser> _userManager;
        private readonly ApplicationDatabaseContext _context;
        private readonly IDirectMessage _messageService;
        private readonly ISaveLog _logService;
        public InboxController(IDirectMessage messageService,ISaveLog logService, UserManager<PanelUser> userManager, ApplicationDatabaseContext context)
        {
            _messageService = messageService;
            _userManager = userManager;
            _context = context;
            _logService = logService;
        }
        public IActionResult Index()
        {
            CurrentPersianDate persianDate = new CurrentPersianDate();
            var user = _userManager.GetUserAsync(User).Result;
            var selectedUser = _context.PanelUsers.Where(u=> u.Id == user.Id)
                .Include(d => d.DirectMessages)
                .ThenInclude(d=>d.ApplicationUser)
                .Include(d=> d.DirectMessages)
                .ThenInclude(d=>d.MessageReplies)
                .FirstOrDefault();
            var messages = selectedUser.DirectMessages.Where(d=>d.IsDeleted.Equals(false));
            var deletedMessages = selectedUser.DirectMessages.Where(d=>d.IsDeleted.Equals(true));
            var replies = _context.MessageReplies.Where(r => r.PanelUser.Id == selectedUser.Id).ToList();

            return View(new MessagesListingViewModel()
            {
                UserDirectMessagesViewModels = messages.Select(d => new UserDirectMessagesViewModel()
                {
                    Id = d.Id,
                    Title = d.Title,
                    ApplicationUserName = d.ApplicationUser.FullName,
                    Content = d.Content,
                    Created = persianDate.GetPersianDate(d.Created),
                    IsRead = d.IsRead,
                    IsDeleted = d.IsDeleted
                }),
                MessageReplyViewModels = replies.Where(r=>r.IsDeleted == false).Select(r=> new MessageReplyViewModel()
                {
                    Id = r.Id,
                    Reply = r.Reply,
                    Created = r.Created,
                    IsDeleted = r.IsDeleted,
                    PanelUserName = selectedUser.UserName,
                    ApplicationUserName = r.DirectMessage.ApplicationUser.FullName
                }),
                UserDeletedDirectMessagesViewModels = deletedMessages.Select(d=> new UserDirectMessagesViewModel()
                {
                    Id = d.Id,
                    Title = d.Title,
                    Content = d.Content,
                    ApplicationUserName = d.ApplicationUser.FullName,
                    Created = d.Created,
                    IsDeleted = d.IsDeleted,
                    IsRead = d.IsRead
                })
            });
        }

        public IActionResult ReplyList(int directMessageId)
        {
            CurrentPersianDate persianDate = new CurrentPersianDate();
            var dm = _messageService.GetDirectMessage(directMessageId);
            TempData["DmId"] = dm.Id;
            MessageRepliesListViewModel viewModel = new()
            {
                Id = dm.Id,
                AppUserName = dm.ApplicationUser.FullName,
                AppUserEmail = dm.ApplicationUser.Email,
                Content = dm.Content,
                Title = dm.Title,
                Created = dm.Created,
                RepliesModel = BuildMessageReplies(dm.MessageReplies),
                NewReply = ""
            };
            return View(viewModel);
        }

        private IEnumerable<MessageRepliesModel> BuildMessageReplies(IEnumerable<MessageReply> messageReplies)
        {
            return messageReplies.Select(reply => new MessageRepliesModel
            {
                Id = reply.Id,
                Reply = reply.Reply,
                Created = reply.Created
            });
        }

        [HttpPost]
        public async Task<IActionResult> Reply(string newReply)
        {
            var dm = _messageService.GetDirectMessage(int.Parse(TempData["DmId"].ToString()));
            var user = _userManager.GetUserAsync(User).Result;
            if (!ModelState.IsValid)
            {
                return View(newReply);
            }
            MessageReply reply = new()
            {
                Reply = newReply,
                Created = DateTime.Now,
                DirectMessage = dm,
                IsDeleted = false,
                PanelUser = user,
            };
            _messageService.AddReply(reply).Wait();
            await _logService.AddLog(new SystemLog()
            {
                Description = $"کاربر {user.UserName} در تاریخ {persianDate.GetPersianDateNow()} پیام با شناسه {reply.Id} را ایجاد کرد.",
                CreatedAt = persianDate.GetPersianDateNow()
            });
            return RedirectToAction(nameof(ReplyList), "Inbox",new { area="Admin", directMessageId= dm.Id} );
        }

    }
}
