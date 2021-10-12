using BadamApplicationAndForum.Data.Dtos;
using BadamApplicationAndForum.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BadamApplicationAndForum.Controllers
{
    [ApiController]
    [Route("api/app/regulations")]
    public class AyinController : Controller
    {
        private readonly IRegulation _regulation;
        public AyinController(IRegulation regulation)
        {
            _regulation = regulation;
        }
        [Authorize(AuthenticationSchemes = MVSJwtTokens.AuthScheme)]
        public ActionResult GetAyins()
        {
            var regs = _regulation.GetRegulations().Select(r=> new RegulationListDto()
            {
                Id = r.Id,
                Name = r.Name,
                Link = r.Link
            });
            return Ok(new { Regulations = regs });
        }
    }
}
