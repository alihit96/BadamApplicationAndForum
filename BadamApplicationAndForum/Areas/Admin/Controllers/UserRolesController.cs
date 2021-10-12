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
using System.Linq;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserRolesController : Controller
    {
        CurrentPersianDate persianDate = new CurrentPersianDate();
        private readonly ISaveLog _logService;
        private readonly SignInManager<PanelUser> _signInManager;
        private readonly UserManager<PanelUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRolesController(ISaveLog logService, UserManager<PanelUser> userManager, SignInManager<PanelUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logService = logService;
        }
        public async Task<IActionResult> Index(string userId)
        {
            var viewModel = new List<UserRolesViewModel>();
            var user = await _userManager.FindByIdAsync(userId);
            foreach (var role in _roleManager.Roles.ToList())
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                viewModel.Add(userRolesViewModel);
            }
            var model = new ManageUserRolesViewModel()
            {
                UserId = userId,
                UserName = user.UserName,
                UserRoles = viewModel
            };
            return View(model);
        }
        public async Task<IActionResult> Update(string id, ManageUserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            result = await _userManager.AddToRolesAsync(user, model.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
            var currentUser = await _userManager.GetUserAsync(User);
            await _signInManager.RefreshSignInAsync(currentUser);
            await Helpers.Seeds.DefaultUsers.SeedAdminAsync(_userManager, _roleManager);
            var theuser = _userManager.GetUserAsync(User).Result;
            await _logService.AddLog(new SystemLog()
            {
                Description = $"کاربر {theuser.UserName} در تاریخ {persianDate.GetPersianDateNow()} نقش های کاربر با شناسه {user.Id} را ویرایش کرد.",
                CreatedAt = persianDate.GetPersianDateNow()
            });
            return RedirectToAction("Index", "UserRoles", new { area = "Admin", userId = id });
        }
    }
}
