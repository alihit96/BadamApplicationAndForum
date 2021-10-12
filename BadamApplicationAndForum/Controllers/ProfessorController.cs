using BadamApplicationAndForum.Data.Dtos;
using BadamApplicationAndForum.Data.Dtos.Professors;
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
    [ApiController]
    public class ProfessorController : Controller
    {
        private readonly IProfessor _professorService;
        private readonly IStaff _staffService;
        public ProfessorController(IProfessor professorService, IStaff staff)
        {
            _professorService = professorService;
            _staffService = staff;
        }
        [HttpGet]
        [Route("api/app/professors")]
        [Authorize(AuthenticationSchemes = MVSJwtTokens.AuthScheme)]
        public ActionResult GetAll()
        {
            
            var professors = _professorService.GetProfessors().Select(p => new ProfessorListingModel
            {
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Email = p.Email,
                EducationLevel = p.EducationLevel,
                Field = p.Field,
                Group = p.Group,
                ScopusLink = p.ScopusLink,
                ScholarLink = p.ScholarLink,
                OrcidLink = p.OrcidLink,
                PublonsLink = p.PublonsLink,
                WosLink = p.WosLink,
                ProfessorInterests = p.ProfessorInterests,
                About = p.About,
                NormalName = p.NormalName,
                OragnizationalObligation = p.OragnizationalObligation
            });
            return Ok(new
            {
                Professors = professors
            });
        }


        [HttpGet]
        [Route("api/app/professor/{id}")]
        [Authorize(AuthenticationSchemes = MVSJwtTokens.AuthScheme)]
        public ActionResult GetProfessor(int id)
        {
            var professor = _professorService.GetProfessor(id);
            if (professor == null)
            {
                return NotFound();
            }
            return Ok(professor);
        }

        [HttpGet]
        [Route("api/app/staffs")]
        [Authorize(AuthenticationSchemes = MVSJwtTokens.AuthScheme)]
        public ActionResult GetStaffs()
        {

            var staff = _staffService.GetStaffs().Select(p => new StaffListingModel
            {
                FullName = p.FullName,
                ImageUrl = p.ImageUrl,
                Email = p.Email,
                NormalName = p.NormalName,
                Department = p.Department,
                Duties = p.Duties,
                Duty = p.Duty,
                Id = p.Id,
                Phone = p.Phone
            });
            return Ok(new
            {
                Staffs = staff
            });
        }

        [HttpGet]
        [Route("api/app/staff/{id}")]
        [Authorize(AuthenticationSchemes = MVSJwtTokens.AuthScheme)]
        public ActionResult GetStaff(int id)
        {
            var staff = _staffService.GetStaff(id);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }
    }
}
