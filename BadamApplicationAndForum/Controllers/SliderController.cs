using BadamApplicationAndForum.Data.Dtos;
using BadamApplicationAndForum.Data.Dtos.News;
using BadamApplicationAndForum.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BadamApplicationAndForum.Controllers
{
    [ApiController]
    [Route("api/app/slider")]
    [Authorize(AuthenticationSchemes = MVSJwtTokens.AuthScheme)]
    public class SliderController : Controller
    {
        private readonly ISlider _sliderService;
        public SliderController(ISlider sliderService)
        {
            _sliderService = sliderService;
        }
        [Authorize(AuthenticationSchemes = MVSJwtTokens.AuthScheme)]
        public ActionResult GetSlider()
        {
            var sliderNews = _sliderService.GetSliders().Select(s => new SliderListingModel
            {
                Id = s.Id,
                ImageUrl = s.ImageUrl,
                Title = s.Title
            });
            var sliderCount = _sliderService.SliserCount();


            return Ok(new
            {
                Sliders = sliderNews,
                Count = sliderCount
            });
        }
    }
}
