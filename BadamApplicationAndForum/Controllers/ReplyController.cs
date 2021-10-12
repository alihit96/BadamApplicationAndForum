using BadamApplicationAndForum.Data.Dtos;
using BadamApplicationAndForum.Data.Dtos.Replies;
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
    [Route("api/app/postreply")]
    public class ReplyController : Controller
    {
        private readonly IReply _replyService;
        private readonly IPost _postService;
        private readonly IApplicationUser _applicationUser;
        public ReplyController(IReply replyService, IPost postService, IApplicationUser applicationUser)
        {
            _replyService = replyService;
            _postService = postService;
            _applicationUser = applicationUser;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = MVSJwtTokens.AuthScheme)]
        public async Task<ActionResult> AddReply(NewReplyModel model)
        {
            var user = _applicationUser.FindUserById(model.AuthorId);
            var userId = user.Id.ToString();
            
            var post = _postService.GetPostById(model.PostId);
            if (post == null)
            {
                return NotFound();
            }
            var reply = BuildReply(model, user, post);

            _replyService.Add(reply).Wait();
            return Ok(new { Id = reply.Id, userID = userId });
        }

        private PostReply BuildReply(NewReplyModel model, ApplicationUser user, Post post)
        {
            return new PostReply
            {
                Content = model.Content,
                Created = DateTime.Now,
                Post = post,
                ApplicationUser = user
            };
        }
    }
}
