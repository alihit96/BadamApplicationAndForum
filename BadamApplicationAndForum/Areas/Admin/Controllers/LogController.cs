
using BadamApplicationAndForum.Data.Interfaces;
using BadamApplicationAndForum.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class LogController : Controller
    {
        private readonly ISaveLog _saveLogService;
        public LogController(ISaveLog saveLogService)
        {
            _saveLogService = saveLogService;
        }
        public IActionResult Index()
        {
            var logs = _saveLogService.GetSystemLogs();
            return View(logs.Select(l=> new LogsListViewModel()
            {
                Id = l.Id,
                Description = l.Description
            }));
        }
    }
}
