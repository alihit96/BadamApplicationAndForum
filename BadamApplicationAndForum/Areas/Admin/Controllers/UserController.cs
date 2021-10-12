using BadamApplicationAndForum.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BadamApplicationAndForum.Data.ViewModels;
using System;
using Microsoft.AspNetCore.Authorization;
using BadamApplicationAndForum.Helpers;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using BadamApplicationAndForum.Data.Interfaces;
using ExamCards.Domain.Entities;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserController : Controller
    {
        CurrentPersianDate persianDate = new CurrentPersianDate();
        private readonly UserManager<PanelUser> _userManager;
        private readonly SignInManager<PanelUser> _signInManager;
        private readonly ISaveLog _logService;
        private readonly IHttpContextAccessor _accessor;

        public UserController(UserManager<PanelUser> userManager, SignInManager<PanelUser> signInManager, IHttpContextAccessor accessor, ISaveLog saveLog)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accessor = accessor;
            _logService = saveLog;
        }
        
        public IActionResult Index()
        {
            var users= _userManager.Users.Select(u=> new UsersListViewModel()
            {
                Id = u.Id,
                MobileNumber = u.PhoneNumber,
                UserName = u.UserName
            });
            return View(users);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserRegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            PanelUser user = new PanelUser()
            {
                PhoneNumber = register.MobileNumber,
                UserName = register.UserName,
            };
            var result = _userManager.CreateAsync(user, register.Password).Result;
            if (result.Succeeded)
            {
                var theUser = _userManager.GetUserAsync(User).Result;
                await _logService.AddLog(new SystemLog()
                {
                    Description = $"کاربر {theUser.UserName} در تاریخ {persianDate.GetPersianDateNow()} کاربری با شناسه {user.Id} را ایجاد کرد.",
                    CreatedAt = persianDate.GetPersianDateNow()
                });
                return RedirectToAction("Index", "user", new { area = "admin"});
            }
            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;

            return View(register);
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            string googleResponse = HttpContext.Request.Form["g-Recaptcha-Response"];
            GoogleRecaptcha recaptcha = new GoogleRecaptcha();
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var user = _userManager.FindByNameAsync(login.UserName).Result;
            if (user != null && recaptcha.Verify(googleResponse) != false)
            {
                _signInManager.SignOutAsync();
                var result = _signInManager.PasswordSignInAsync(user, login.Password, login.IsPersistent, true).Result;
                if (result.Succeeded)
                {
                    _logService.AddLog(new SystemLog()
                    {
                        Description = $"کاربر با آدرس IP {_accessor.HttpContext.Connection.RemoteIpAddress} و نام کابری {user.UserName} در تاریخ {DateTime.Now} به سیستم وارد شد",
                        CreatedAt = persianDate.GetPersianDateNow()
                    }).Wait();
                    return RedirectToAction("Index", "user", new { area = "admin" });
                }
                else if (result.IsLockedOut)
                {
                    TempData["LockedOut"] = "حساب شما به مدت 10 دقیق مسدود شده است.";
                    _logService.AddLog(new SystemLog()
                    {
                        Description = $"دسترسی کاربر با آدرس IP {_accessor.HttpContext.Connection.RemoteIpAddress} و نام کابری {user.UserName} در تاریخ {DateTime.Now} به مدت 10 دقیقه مسدود شد",
                        CreatedAt = persianDate.GetPersianDateNow()
                    }).Wait();
                }
            }
            else if (recaptcha.Verify(googleResponse) == false)
            {
                ViewBag.Result = "لطفا بر روی من ربات نیستم کلیک کنید";
                return View(login);
            }
            else
            {
                ViewBag.Result = "کاربر پیدا نشد";
            }

            return View(login);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "User", new { area = "Admin" });

        }


        public IActionResult Edit(string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;
            if (user == null)
            {
                return RedirectToAction("NotFoundOrDeleted", "Feed", new { area = "Admin" });
            }
            UserEditViewModel userEdit = new UserEditViewModel()
            {
                UserName = user.UserName,
                MobileNumber = user.PhoneNumber
            };
            return View(userEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel userEditDto)
        {
            var user = _userManager.FindByIdAsync(userEditDto.Id).Result;
            user.UserName = userEditDto.UserName;
            user.PhoneNumber = userEditDto.MobileNumber;
            var editUser = _userManager.UpdateAsync(user).Result;
            if (editUser.Succeeded)
            {
                var theuser = _userManager.GetUserAsync(User).Result;
                await _logService.AddLog(new SystemLog()
                {
                    Description = $"کاربر {theuser.UserName} در تاریخ {persianDate.GetPersianDateNow()} کاربر با شناسه {user.Id} را ویرایش کرد.",
                    CreatedAt = persianDate.GetPersianDateNow()
                });
                return RedirectToAction("Index", "User", new { area = "admin" });
            }
            string message = "";
            foreach (var item in editUser.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;
            return View(editUser);
        }

        public IActionResult Delete(string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;
            if (user == null)
            {
                return RedirectToAction("NotFoundOrDeleted", "Feed", new { area = "Admin" });
            }
            UserDeleteViewModel userDelete = new UserDeleteViewModel()
            {
                Id = user.Id,
                MobileNumber = user.PhoneNumber,
                UserName = user.UserName,
            };
            return View(userDelete);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteViewModel deleteDto)
        {
            var user = _userManager.FindByIdAsync(deleteDto.Id).Result;
            var result = _userManager.DeleteAsync(user).Result;

            if (result.Succeeded)
            {
                var theuser = _userManager.GetUserAsync(User).Result;
                await _logService.AddLog(new SystemLog()
                {
                    Description = $"کاربر {theuser.UserName} در تاریخ {persianDate.GetPersianDateNow()} کاربر با شناسه {user.Id} را حذف کرد.",
                    CreatedAt = persianDate.GetPersianDateNow()
                });
                if (user.UserName == User.Identity.Name)
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Login", "User", new { area = "admin" });
                }
                return RedirectToAction("Index", "User", new { area = "admin" });
            }
            
            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;

            return View(deleteDto);
        }

        public IActionResult Details(string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;
            if (user == null)
            {
                return RedirectToAction("NotFoundOrDeleted", "Feed", new { area = "Admin" });
            }
            UserDetailViewModel userDetail = new UserDetailViewModel()
            {
                Id = user.Id,
                MobileNumber = user.PhoneNumber,
                UserName = user.UserName,
            };
            return View(userDetail);
        }
    }
}
