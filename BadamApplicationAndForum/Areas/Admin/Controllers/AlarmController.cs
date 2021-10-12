using BadamApplicationAndForum.Data.Interfaces;
using BadamApplicationAndForum.Data.Models;
using BadamApplicationAndForum.Data.ViewModels;
using BadamApplicationAndForum.Helpers;
using ExamCards.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AlarmController : Controller
    {
        private readonly IApplicationUser _user;
        private readonly IAlarm _alarm;
        CurrentPersianDate persianDate = new CurrentPersianDate();
        private readonly UserManager<PanelUser> _userManager;
        private readonly ISaveLog _logService;
        public AlarmController(IApplicationUser user, IAlarm alarm, UserManager<PanelUser> userManager, ISaveLog logService)
        {
            _user = user;
            _alarm = alarm;
            _logService = logService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var alarms = _alarm.GetAlarms();
            return View(alarms.Select(a=> new AlarmsListViewModel
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                Date = a.Date,
                ApplicationUser = a.ApplicationUser.FullName
            }));
        }

        public IActionResult Create()
        {
            var users = _user.GetUsers();
            var userNames = new List<SelectListItem>(_user.GetUsers().Select(u => new SelectListItem()
            {
                Text = u.FullName,
                Value = u.FullName
            }).ToList());
            return View(new AlarmCreateViewModel()
            {
                UserNames = userNames,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(AlarmCreateViewModel viewModel)
        {
            var user = _user.FindUserByFullName(viewModel.UserName);

            Alarm alarm = new Alarm()
            {
                Title = viewModel.Title,
                Content = viewModel.Content,
                ApplicationUser = user,
                Date = viewModel.Date
            };
            _alarm.AddAlarm(alarm).Wait();
            var theuser = _userManager.GetUserAsync(User).Result;
            await _logService.AddLog(new SystemLog()
            {
                Description = $"کاربر {theuser.UserName} در تاریخ {persianDate.GetPersianDateNow()} اعلان {alarm.Id} را برای {user.Id} ارسال کرد.",
                CreatedAt = persianDate.GetPersianDateNow()
            });
            FilteredNotificationSender.sendNotification(viewModel.Title, viewModel.Content, user.PusheId);

            return RedirectToAction("Index", "Alarm");
        }
    }
}
