using BadamApplicationAndForum.Data.Dtos;
using BadamApplicationAndForum.Data.Dtos.News;
using BadamApplicationAndForum.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Controllers
{
    [ApiController]
    [Route("api/app/news")]
    
    public class NewsController : Controller
    {
        private readonly INews _newsService;
        public NewsController(INews newsService)
        {
            _newsService = newsService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = MVSJwtTokens.AuthScheme)]
        public ActionResult GetNews()
        {
            var news = _newsService.GetAllNews().Select(n => new NewsListingModel
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                ImageUrl = n.ImageUrl,
                Date = n.Date,
                Group = n.Group,
                FeedUrl = n.FeedUrl
                
            });
            var newsCount = _newsService.NewsCount();
            return Ok(new
            {
                News = news,
                Count = newsCount
            });
        }
    }
}
