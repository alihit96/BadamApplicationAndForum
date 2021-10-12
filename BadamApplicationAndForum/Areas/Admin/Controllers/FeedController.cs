using BadamApplicationAndForum.Data.Dtos.News;
using BadamApplicationAndForum.Data.Interfaces;
using BadamApplicationAndForum.Data.Models;
using BadamApplicationAndForum.Data.ViewModels;
using BadamApplicationAndForum.Helpers;
using ExamCards.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pushe.co;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class FeedController : Controller
    {
        CurrentPersianDate persianDate = new CurrentPersianDate();
        private readonly INews _newsService; 
        private readonly UserManager<PanelUser> _userManager; 
        private readonly ISaveLog _logService;
        private readonly IPusheService _pusheService;
        public FeedController(INews newsService, ISaveLog saveLog, UserManager<PanelUser> userManager)
        {
            _newsService = newsService;
            _logService = saveLog;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var news = _newsService.GetAllNews();
            return View(news.Select(n=> new NewsListingModel()
            {
                Id = n.Id,
                Content = n.Content,
                Date = n.Date,
                ImageUrl = n.ImageUrl,
                Title = n.Title,
                Group = n.Group
            }));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(FeedCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            string fileName = "";

            if (viewModel.ImageUrl != null)
            {
                var extension = new StringBuilder(".").Append(viewModel.ImageUrl.FileName.Split(".")[viewModel.ImageUrl.FileName.Split(".").Length - 1]);
                fileName = new StringBuilder(Guid.NewGuid().ToString()).Append(extension).ToString();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\FeedImages", fileName);
                if (path.ToLower().Contains("png") || path.ToLower().Contains("jpg"))
                {
                    using (var bits = new FileStream(path, FileMode.Create))
                    {
                        await viewModel.ImageUrl.CopyToAsync(bits);
                        bits.Close();
                    }
                }
                else
                {
                    TempData["FileIncompatible"] = "لطفا فقط فایل های با فرمت jpg یا png استفاده کنید.";
                    return View(viewModel);
                }
            }
            News news = new News()
            {
                Title = viewModel.Title,
                Content = viewModel.Content,
                Date = viewModel.Date,
                Group = viewModel.Group,
                ImageUrl = fileName,
                ReceivingGroup = viewModel.ReceivingGroup,
                FeedUrl = viewModel.FeedUrl
            };
            await _newsService.AddFeed(news);
            NotificationSender.sendNotification(title: viewModel.Title, content: viewModel.Content, topics: new String[] { viewModel.ReceivingGroup });

            var user = _userManager.GetUserAsync(User).Result;
            await _logService.AddLog(new SystemLog()
            {
                Description = $"کاربر {user.UserName} در تاریخ {persianDate.GetPersianDateNow()} خبر با شناسه {news.Id} را ایجاد کرد.",
                CreatedAt = persianDate.GetPersianDateNow()
            });
            return RedirectToAction("Index", "Feed", new { area = "admin" });
        }

        public IActionResult Edit(int Id)
        {
            var feed = _newsService.GetFeed(Id);
            if (feed == null)
            {
                return RedirectToAction("NotFoundOrDeleted", "Feed", new { area = "Admin" });
            }
            FeedEditViewModel viewModel = new FeedEditViewModel()
            {
                Id = feed.Id,
                Content = feed.Content,
                Title = feed.Title,
                Date = feed.Date,
                Group = feed.Group,
                ImageUrlName = feed.ImageUrl,
                FeedUrl = feed.FeedUrl
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(FeedEditViewModel viewModel)
        {
            var feed = _newsService.GetFeed(viewModel.Id);
            feed.Title = viewModel.Title;
            feed.Content = viewModel.Content;
            feed.Date = viewModel.Date;
            feed.Group = viewModel.Group;
            feed.FeedUrl = viewModel.FeedUrl;

            string fileName = "";

            if (viewModel.ImageUrl != null)
            {
                var extension = new StringBuilder(".").Append(viewModel.ImageUrl.FileName.Split(".")[viewModel.ImageUrl.FileName.Split(".").Length - 1]);
                fileName = new StringBuilder(Guid.NewGuid().ToString()).Append(extension).ToString();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\FeedImages", fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await viewModel.ImageUrl.CopyToAsync(bits);
                    bits.Close();
                }

                feed.ImageUrl = fileName;
            }
            await _newsService.UpdateFeed(feed);

            var user = _userManager.GetUserAsync(User).Result;
            await _logService.AddLog(new SystemLog()
            {
                Description = $"کاربر {user.UserName} در تاریخ {persianDate.GetPersianDateNow()} خبر با شناسه {feed.Id} را ویرایش کرد.",
                CreatedAt = persianDate.GetPersianDateNow()
            });
            return RedirectToAction("Index", "Feed", new { area = "Admin" });
        }

        public IActionResult Delete(int Id)
        {
            var feed = _newsService.GetFeed(Id);
            if (feed == null)
            {
                return RedirectToAction("NotFoundOrDeleted", "Feed", new { area = "Admin" });
            }
            FeedDeleteViewModel viewModel = new FeedDeleteViewModel()
            {
                Id = feed.Id,
                Title = feed.Title,
                Content = feed.Content,
                Date = feed.Date,
                Group = feed.Group,
                ImageUrl = feed.ImageUrl,
                FeedUrl = feed.FeedUrl
                
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(FeedDeleteViewModel viewModel)
        {
            var feed = _newsService.GetFeed(viewModel.Id);
            if (feed == null)
            {
                return RedirectToAction("NotFoundOrDeleted","Feed", new { area = "Admin"});
            }
            await _newsService.DeleteFeed(feed);
            var user = _userManager.GetUserAsync(User).Result;
            await _logService.AddLog(new SystemLog()
            {
                Description = $"کاربر {user.UserName} در تاریخ {persianDate.GetPersianDateNow()} خبر با شناسه {feed.Id} را حذف کرد.",
                CreatedAt = persianDate.GetPersianDateNow()
            });
            return RedirectToAction("Index", "Feed", new { area = "Admin" });
        }

        public IActionResult Details(int Id)
        {
            var feed = _newsService.GetFeed(Id);
            if (feed == null)
            {
                return RedirectToAction("NotFoundOrDeleted", "Feed", new { area = "Admin" });
            }
            FeedDetailViewModel viewModel = new FeedDetailViewModel()
            {
                Id = feed.Id,
                Title = feed.Title,
                Content = feed.Content,
                Date = feed.Date,
                Group = feed.Group,
                ImageUrl = feed.ImageUrl,
                FeedUrl = feed.FeedUrl
                
            };
            return View(viewModel);
        }

        public IActionResult NotFoundOrDeleted()
        {
            return View();
        }
    }
}