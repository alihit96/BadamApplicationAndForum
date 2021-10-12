using BadamApplicationAndForum.Data.Interfaces;
using BadamApplicationAndForum.Data.Models;
using BadamApplicationAndForum.Data.ViewModels;
using BadamApplicationAndForum.Helpers;
using ExamCards.Domain.Entities;
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
    public class PersonelController : Controller
    {
        private readonly IProfessor _professorService;
        private readonly IStaff _staffService;
        CurrentPersianDate persianDate = new CurrentPersianDate();
        private readonly UserManager<PanelUser> _userManager;
        private readonly ISaveLog _logService;

        public PersonelController(IProfessor professor, IStaff staffService, ISaveLog logService, UserManager<PanelUser> userManager)
        {
            _professorService = professor;
            _staffService = staffService;
            _logService = logService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var professors = _professorService.GetProfessors();
            return View(professors.Select(p => new ProfessorsListViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Email = p.Email,
                Group = p.Group,
                
            }));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProfessorCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            string fileName = "";

            var extension = new StringBuilder(".").Append(viewModel.ImageUrl.FileName.Split(".")[viewModel.ImageUrl.FileName.Split(".").Length - 1]);
            fileName = new StringBuilder(Guid.NewGuid().ToString()).Append(extension).ToString();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\ProfessorImages", fileName);

            using (var bits = new FileStream(path, FileMode.Create))
            {
                await viewModel.ImageUrl.CopyToAsync(bits);
            }

            Professor professor = new()
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                EducationLevel = viewModel.EducationLevel,
                Field = viewModel.Field,
                Group = viewModel.Group,
                ImageUrl = fileName,
                Phone = viewModel.Phone,
                OragnizationalObligation = viewModel.OragnizationalObligation,
                About = viewModel.About,
                ProfessorInterests = viewModel.ProfessorInterests,
                OrcidLink = viewModel.OrcidLink,
                PublonsLink = viewModel.PublonsLink,
                ScholarLink = viewModel.ScholarLink,
                ScopusLink = viewModel.ScopusLink,
                WosLink = viewModel.WosLink,
                NormalName = viewModel.NormalName
            };
            _professorService.AddProfessor(professor).Wait();
            var user = _userManager.GetUserAsync(User).Result;
            await _logService.AddLog(new SystemLog()
            {
                Description = $"کاربر {user.UserName} در تاریخ {persianDate.GetPersianDateNow()} استاد با شناسه {professor.Id} را ایجاد کرد.",
                CreatedAt = persianDate.GetPersianDateNow()
            });
            return RedirectToAction("Details", "Personel", new { admin = "Admin", Id= professor.Id });
        }

        public IActionResult Edit(int Id)
        {
            var professor = _professorService.GetProfessor(Id);
            ProfessorEditViewModel viewModel = new()
            {
                Id = professor.Id,
                EducationLevel = professor.EducationLevel,
                Email = professor.Email,
                Field = professor.Field,
                Group = professor.Group,
                Name = professor.Name,
                Phone = professor.Phone,
                OragnizationalObligation = professor.OragnizationalObligation,
                About = professor.About,
                NormalName = professor.NormalName,
                OrcidLink = professor.OrcidLink,
                PublonsLink = professor.PublonsLink,
                ScholarLink = professor.ScholarLink,
                ScopusLink = professor.ScopusLink,
                WosLink = professor.WosLink,
                ProfessorInterests = professor.ProfessorInterests
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProfessorEditViewModel viewModel)
        {

            var professor = _professorService.GetProfessor(viewModel.Id);
            professor.Name = viewModel.Name;
            professor.EducationLevel = viewModel.EducationLevel;
            professor.Email = viewModel.Email;
            professor.WosLink = viewModel.WosLink;
            professor.Field = viewModel.Field;
            professor.Group = viewModel.Group;
            professor.About = viewModel.About;
            professor.OragnizationalObligation = viewModel.OragnizationalObligation;
            professor.Phone = viewModel.Phone;
            professor.ScholarLink = viewModel.ScholarLink;
            professor.ScopusLink = viewModel.ScopusLink;
            professor.OrcidLink = viewModel.OrcidLink;
            professor.ProfessorInterests = viewModel.ProfessorInterests;
            professor.NormalName = viewModel.NormalName;
            professor.PublonsLink = viewModel.PublonsLink;

            string fileName = "";

            if (viewModel.ImageUrl != null)
            {
                var extension = new StringBuilder(".").Append(viewModel.ImageUrl.FileName.Split(".")[viewModel.ImageUrl.FileName.Split(".").Length - 1]);
                fileName = new StringBuilder(Guid.NewGuid().ToString()).Append(extension).ToString();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\professorImages", fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await viewModel.ImageUrl.CopyToAsync(bits);
                    bits.Close();
                }
                professor.ImageUrl = fileName;
            }

            await _professorService.UpdateProfessor(professor);
            var user = _userManager.GetUserAsync(User).Result;
            await _logService.AddLog(new SystemLog()
            {
                Description = $"کاربر {user.UserName} در تاریخ {persianDate.GetPersianDateNow()} خبر با شناسه {professor.Id} را ویرایش کرد.",
                CreatedAt = persianDate.GetPersianDateNow()
            });
            return RedirectToAction("Details", "Personel", new { area = "Admin", Id = professor.Id });
        }

        public IActionResult Delete(int Id)
        {
            var professor = _professorService.GetProfessor(Id);
            ProfessorDeleteViewModel viewModel = new()
            {
                Id = professor.Id,
                EducationLevel = professor.EducationLevel,
                Email = professor.Email,
                Field = professor.Field,
                Group = professor.Group,
                ImageUrl = professor.ImageUrl,
                Name = professor.Name,
                OragnizationalObligation = professor.OragnizationalObligation,
                Phone = professor.Phone,
                NormalName = professor.NormalName,
                OrcidLink = professor.OrcidLink,
                PublonsLink = professor.PublonsLink,
                ScholarLink = professor.ScholarLink,
                ScopusLink = professor.ScopusLink,
                WosLink = professor.WosLink,
                ProfessorInterests = professor.ProfessorInterests
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProfessorDeleteViewModel viewModel)
        {
            var professor = _professorService.GetProfessor(viewModel.Id);
            _professorService.DeleteProfessor(professor.Id).Wait();
            var user = _userManager.GetUserAsync(User).Result;
            await _logService.AddLog(new SystemLog()
            {
                Description = $"کاربر {user.UserName} در تاریخ {persianDate.GetPersianDateNow()} خبر با شناسه {professor.Id} را حذف کرد.",
                CreatedAt = persianDate.GetPersianDateNow()
            });
            return RedirectToAction("Index", "Personel", new { area = "Admin" });
        }


        public IActionResult Details(int Id)
        {
            var professor = _professorService.GetProfessor(Id);
            ProfessorDetailViewModel viewModel = new()
            {
                Id = professor.Id,
                EducationLevel = professor.EducationLevel,
                Email = professor.Email,
                Phone = professor.Phone,
                Field = professor.Field,
                Group = professor.Group,
                ImageUrl = professor.ImageUrl,
                OragnizationalObligation = professor.OragnizationalObligation,
                About = professor.About,
                Name = professor.Name,
                NormalName = professor.NormalName,
                OrcidLink = professor.OrcidLink,
                ProfessorInterests = professor.ProfessorInterests,
                PublonsLink = professor.PublonsLink,
                ScholarLink = professor.ScholarLink,
                ScopusLink = professor.ScopusLink,
                WosLink = professor.WosLink
            };
            return View(viewModel);
        }


        public IActionResult StaffIndex()
        {
            var staffs = _staffService.GetStaffs();
            return View(staffs.Select(p => new StaffListViewModel()
            {
                Id = p.Id,
                FullName = p.FullName,
                ImageUrl = p.ImageUrl,
                Email = p.Email,
                Department = p.Department,
                Duty = p.Duty,
                Phone = p.Phone
            }));
        }


        public IActionResult AddStaff()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStaff(StaffCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            string fileName = "";

            var extension = new StringBuilder(".").Append(viewModel.ImageUrl.FileName.Split(".")[viewModel.ImageUrl.FileName.Split(".").Length - 1]);
            fileName = new StringBuilder(Guid.NewGuid().ToString()).Append(extension).ToString();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\StaffImages", fileName);

            using (var bits = new FileStream(path, FileMode.Create))
            {
                await viewModel.ImageUrl.CopyToAsync(bits);
                bits.Close();
            }

            Staff staff = new()
            {
                FullName = viewModel.FullName,
                Duty = viewModel.Duty,
                Duties = viewModel.Duties,
                Email = viewModel.Email,
                ImageUrl = fileName,
                NormalName = viewModel.NormalName,
                Phone = viewModel.Phone,
                Department = viewModel.Department
            };
            _staffService.Add(staff).Wait();
            var user = _userManager.GetUserAsync(User).Result;
            await _logService.AddLog(new SystemLog()
            {
                Description = $"کاربر {user.UserName} در تاریخ {persianDate.GetPersianDateNow()} پرسنل با شناسه {staff.Id} را ایجاد کرد.",
                CreatedAt = persianDate.GetPersianDateNow()
            });
            return RedirectToAction("StaffDetails", "Personel", new { area="Admin", Id = staff.Id });
        }

        public IActionResult EditStaff(int Id)
        {
            var staff = _staffService.GetStaff(Id);


            StaffEditViewModel viewModel = new()
            {
                Id = staff.Id,
                Email = staff.Email,
                Phone = staff.Phone,
                Duty = staff.Duty,
                NormalName = staff.NormalName,
                Duties = staff.Duties,
                FullName = staff.FullName,
                Department = staff.Department
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditStaff(StaffEditViewModel viewModel)
        {

            var staff = _staffService.GetStaff(viewModel.Id);
            staff.FullName = viewModel.FullName;
            staff.Department = viewModel.Department;
            staff.Duty = viewModel.Duty;
            staff.Duties = viewModel.Duties;
            staff.Email = viewModel.Email;
            staff.Phone = viewModel.Phone;
            staff.NormalName = viewModel.NormalName;

            string fileName = "";

            if (viewModel.ImageUrl != null)
            {
                var extension = new StringBuilder(".").Append(viewModel.ImageUrl.FileName.Split(".")[viewModel.ImageUrl.FileName.Split(".").Length - 1]);
                fileName = new StringBuilder(Guid.NewGuid().ToString()).Append(extension).ToString();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\StaffImages", fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await viewModel.ImageUrl.CopyToAsync(bits);
                    bits.Close();
                }
                staff.ImageUrl = fileName;
            }

            await _staffService.Update(staff);
            var user = _userManager.GetUserAsync(User).Result;
            await _logService.AddLog(new SystemLog()
            {
                Description = $"کاربر {user.UserName} در تاریخ {persianDate.GetPersianDateNow()} پرسنل با شناسه {staff.Id} را ویرایش کرد.",
                CreatedAt = persianDate.GetPersianDateNow()
            });
            return RedirectToAction("StaffDetails", "Personel", new { area = "Admin", Id = staff.Id });
        }

        public IActionResult StaffDetails(int Id)
        {
            var staff = _staffService.GetStaff(Id);

            StaffDetailsViewModel viewModel = new()
            {
                Id = staff.Id,
                Email = staff.Email,
                Phone = staff.Phone,
                ImageUrl = staff.ImageUrl,
                FullName = staff.FullName,
                NormalName = staff.NormalName,
                Duties = staff.Duties,
                Duty = staff.Duty,
                Department = staff.Department
            };
            return View(viewModel);
        }

        public IActionResult DeleteStaff(int Id)
        {
            var staff = _staffService.GetStaff(Id);
            StaffDeleteViewModel viewModel = new()
            {
                Id = staff.Id,
                Email = staff.Email,
                Department = staff.Department,
                ImageUrl = staff.ImageUrl,
                FullName = staff.FullName,
                Phone = staff.Phone,
                NormalName = staff.NormalName,
                Duty = staff.Duty,
                Duties = staff.Duties
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStaff(StaffDeleteViewModel viewModel)
        {
            var staff = _staffService.GetStaff(viewModel.Id);
            _professorService.DeleteProfessor(staff.Id).Wait();
            var user = _userManager.GetUserAsync(User).Result;
            await _logService.AddLog(new SystemLog()
            {
                Description = $"کاربر {user.UserName} در تاریخ {persianDate.GetPersianDateNow()} پرسنل با شناسه {staff.Id} را حذف کرد.",
                CreatedAt = persianDate.GetPersianDateNow()
            });
            return RedirectToAction("StaffIndex", "Personel", new { area = "Admin" });
        }

    }
}
