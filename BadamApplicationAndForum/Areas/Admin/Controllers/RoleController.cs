using BadamApplicationAndForum.Data.Interfaces;
using BadamApplicationAndForum.Data.Models;
using BadamApplicationAndForum.Data.ViewModels;
using BadamApplicationAndForum.Helpers;
using ExamCards.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        CurrentPersianDate persianDate = new CurrentPersianDate();
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<PanelUser> _userManager;
        private readonly ISaveLog _logService;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<PanelUser> userManager, ISaveLog logService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logService = logService;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
                var user = _userManager.GetUserAsync(User).Result;
                await _logService.AddLog(new SystemLog()
                {
                    Description = $"کاربر {user.UserName} در تاریخ {persianDate.GetPersianDateNow()} نقش {roleName} را ایجاد کرد.",
                    CreatedAt = persianDate.GetPersianDateNow()
                });
            }
            return RedirectToAction("Index", "Role", new { area = "Admin" });
        }

        public IActionResult Edit(string Id)
        {
            var role = _roleManager.FindByIdAsync(Id).Result;
            RoleEditDto userEdit = new RoleEditDto()
            {
                Name = role.Name
            };
            return View(userEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleEditDto roleEditDto)
        {
            var role = _roleManager.FindByIdAsync(roleEditDto.Id).Result;
            role.Name = roleEditDto.Name;

            var editUser = _roleManager.UpdateAsync(role).Result;
            if (editUser.Succeeded)
            {
                var user = _userManager.GetUserAsync(User).Result;
                await _logService.AddLog(new SystemLog()
                {
                    Description = $"کاربر {user.UserName} در تاریخ {persianDate.GetPersianDateNow()} نقش {role.Name} را ویرایش کرد.",
                    CreatedAt = persianDate.GetPersianDateNow()
                });
                return RedirectToAction("Index", "Role", new { area = "Admin" });
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
            var role = _roleManager.FindByIdAsync(Id).Result;
            RoleDeleteDto deleteDto = new RoleDeleteDto()
            {
                Id = role.Id,
                RoleName = role.Name
            };
            return View(deleteDto);

        }
        [HttpPost]
        public async Task<IActionResult> Delete(RoleDeleteDto deleteDto)
        {
            var role = _roleManager.FindByIdAsync(deleteDto.Id).Result;
            var result = _roleManager.DeleteAsync(role).Result;
            if (result.Succeeded)
            {
                var user = _userManager.GetUserAsync(User).Result;
                await _logService.AddLog(new SystemLog()
                {
                    Description = $"کاربر {user.UserName} در تاریخ {persianDate.GetPersianDateNow()} نقش {role.Name} را حذف کرد.",
                    CreatedAt = persianDate.GetPersianDateNow()
                });
                return RedirectToAction("Index", "Role", new { area = "Admin" });
            }
            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;

            return View(deleteDto);

        }
    }
}
