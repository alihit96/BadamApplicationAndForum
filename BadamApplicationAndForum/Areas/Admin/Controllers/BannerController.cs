using BadamApplicationAndForum.Data.Interfaces;
using BadamApplicationAndForum.Data.Models;
using BadamApplicationAndForum.Data.ViewModels;
using BadamApplicationAndForum.Helpers;
using ExamCards.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class BannerController : Controller
    {
        CurrentPersianDate persianDate = new CurrentPersianDate();
        private readonly ISlider _sliderService;
        private readonly UserManager<PanelUser> _userManager;
        private readonly ISaveLog _logService;
        public BannerController(ISlider sliderService, UserManager<PanelUser> userManager, ISaveLog saveLog)
        {
            _sliderService = sliderService;
            _userManager = userManager;
            _logService = saveLog;
        }
        public IActionResult Index()
        {
            var sliders = _sliderService.GetSliders();
            return View(sliders.Select(s=> new SlidersListViewModel()
            {
                Id=s.Id,
                Title=s.Title,
                ImageUrl=s.ImageUrl
            }));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateViewModel viewModel)
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
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\SliderImages", fileName);
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
            Slider slider = new Slider()
            {
                Title = viewModel.Title,
                ImageUrl = fileName
            };
            await _sliderService.AddSlider(slider);
            //TODO: Save log
            var user = _userManager.GetUserAsync(User).Result;
            await _logService.AddLog(new SystemLog()
            {
                Description = $"کاربر {user.UserName} در تاریخ {persianDate.GetPersianDateNow()} بنر با شناسه {slider.Id} را ایجاد کرد.",
                CreatedAt = persianDate.GetPersianDateNow()
            });
            return RedirectToAction("Index", "Banner", new { area = "admin" });
        }
        public IActionResult Edit(int Id)
        {
            var slider = _sliderService.GetSlider(Id);
            if (slider == null)
            {
                return RedirectToAction("NotFoundOrDeleted", "Feed", new { area = "Admin" });
            }
            SliderEditViewModel viewModel = new SliderEditViewModel()
            {
                Id = slider.Id,
                Title=slider.Title,
                ImageUrlName= slider.ImageUrl
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SliderEditViewModel viewModel)
        {
            var slider = _sliderService.GetSlider(viewModel.Id);
            slider.Title = viewModel.Title;

            string fileName = "";

            if (viewModel.ImageUrl != null)
            {
                var extension = new StringBuilder(".").Append(viewModel.ImageUrl.FileName.Split(".")[viewModel.ImageUrl.FileName.Split(".").Length - 1]);
                fileName = new StringBuilder(Guid.NewGuid().ToString()).Append(extension).ToString();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\SliderImages", fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await viewModel.ImageUrl.CopyToAsync(bits);
                }
                slider.ImageUrl = fileName;
            }
            await _sliderService.UpdateSlider(slider);

            var user = _userManager.GetUserAsync(User).Result;
            await _logService.AddLog(new SystemLog()
            {
                Description = $"کاربر {user.UserName} در تاریخ {persianDate.GetPersianDateNow()} بنر با شناسه {slider.Id} را ویرایش کرد.",
                CreatedAt = persianDate.GetPersianDateNow()
            });
            return RedirectToAction("Index", "Banner", new { area = "Admin" });
        }

        public IActionResult Delete(int Id)
        {
            var slider = _sliderService.GetSlider(Id);
            if (slider == null)
            {
                return RedirectToAction("NotFoundOrDeleted", "Feed", new { area = "Admin" });
            }
            SliderDeleteViewModel viewModel = new SliderDeleteViewModel()
            {
                Id = slider.Id,
                Title = slider.Title,
                ImageUrl = slider.ImageUrl
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(SliderDeleteViewModel viewModel)
        {
            var slider = _sliderService.GetSlider(viewModel.Id);
            await _sliderService.DeleteSlider(slider.Id);
            var user = _userManager.GetUserAsync(User).Result;
            await _logService.AddLog(new SystemLog()
            {
                Description = $"کاربر {user.UserName} در تاریخ {persianDate.GetPersianDateNow()} بنر با شناسه {slider.Id} را حذف کرد.",
                CreatedAt = persianDate.GetPersianDateNow()
            });
            return RedirectToAction("Index", "Banner", new { area = "Admin" });
        }

        public IActionResult Details(int Id)
        {
            var slider = _sliderService.GetSlider(Id);
            if (slider == null)
            {
                return RedirectToAction("NotFoundOrDeleted", "Feed", new { area = "Admin" });
            }
            SliderDeleteViewModel viewModel = new SliderDeleteViewModel()
            {
                Id = slider.Id,
                Title = slider.Title,
                ImageUrl = slider.ImageUrl
            };
            return View(viewModel);
        }
    }
}
