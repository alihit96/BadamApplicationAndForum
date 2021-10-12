using BadamApplicationAndForum.Data.Interfaces;
using BadamApplicationAndForum.Data.Models;
using BadamApplicationAndForum.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RegulationController : Controller
    {
        private readonly IRegulation _regulation;
        public RegulationController(IRegulation regulation)
        {
            _regulation = regulation;
        }
        public IActionResult Index()
        {
            var regs = _regulation.GetRegulations();
            return View(regs.Select(r=> new RegulationListViewModel()
            {
                Id = r.Id,
                Name = r.Name,
                Link = r.Link
            }));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RegulationCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            Regulation regulation = new()
            {
                Name = viewModel.Name,
                Link = viewModel.Link
            };
            _regulation.Add(regulation).Wait();
            return RedirectToAction("Index", "Regulation", new { area = "Admin" });
        }
        public IActionResult Edit(int Id)
        {
            var reg = _regulation.GetRegulation(Id);
            RegulationEditViewModel viewModel = new()
            {
                Id = reg.Id,
                Name = reg.Name,
                Link = reg.Link
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(RegulationEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var reg = _regulation.GetRegulation(viewModel.Id);
            reg.Name = viewModel.Name;
            reg.Link = viewModel.Link;
             _regulation.Update(reg).Wait();
            return RedirectToAction("Details", "Regulation", new { area = "Admin", Id = reg.Id });
        }

        public IActionResult Delete(int Id)
        {
            var reg = _regulation.GetRegulation(Id);
            RegulationDeleteViewModel viewModel = new()
            {
                Id = reg.Id,
                Name = reg.Name,
                Link = reg.Link
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(RegulationEditViewModel viewModel)
        {
            var reg = _regulation.GetRegulation(viewModel.Id);
            _regulation.Delete(reg).Wait();
            return RedirectToAction("Index", "Regulation", new { area = "Admin" });
        }

        public IActionResult Details(int Id)
        {
            var reg = _regulation.GetRegulation(Id);
            RegulationDetailsViewModel viewModel = new()
            {
                Id = reg.Id,
                Name = reg.Name,
                Link = reg.Link
            };
            return View(viewModel);
        }

    }
}
