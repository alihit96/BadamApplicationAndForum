using BadamApplicationAndForum.Data.Dtos;
using BadamApplicationAndForum.Data.Dtos.Posts;
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
    [Route("api/app/posts")]
    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly IForum _forumService;
        private readonly IApplicationUser _applicationUser;


        public PostController(IPost postService, IForum forumService, IApplicationUser applicationUser)
        {
            _postService = postService;
            _forumService = forumService;
            _applicationUser = applicationUser;
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = MVSJwtTokens.AuthScheme)]
        public ActionResult GetPost(int id)
        {
            var post = _postService.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            var replies = BuildPostReplies(post.PostReplies);
            var model = new PostIndexModel
            {
                Id = post.Id,
                AuthorId = post.ApplicationUser.Id.ToString(),
                AuthorName = post.ApplicationUser.FullName,
                Content = post.Content,
                Created = post.Created,
                Title = post.Title,
                Replies = replies
            };
            return Ok(model);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes =MVSJwtTokens.AuthScheme)]
        public async Task<ActionResult> AddPost(NewPostModel model)
        {
            var user = _applicationUser.FindUserById(model.AuthorId);
            var userId = user.Id.ToString();
            var post = BuildPost(model, user);

            await _postService.Add(post);
            return Ok(new { Id = post.Id, userID = userId });
        }

        private Post BuildPost(NewPostModel model, ApplicationUser user)
        {
            var forum = _forumService.GetForumById(model.ForumId);
            return new Post
            {
                Title = model.Title,
                Content = model.Content,
                Created = DateTime.Now,
                ApplicationUser = user,
                Forum = forum
            };
        }

        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply> postReplies)
        {
            return postReplies.Select(reply => new PostReplyModel
            {
                Id = reply.Id,
                Content = reply.Content,
                AuthorId = reply.ApplicationUser.Id.ToString(),
                AuthorName = reply.ApplicationUser.FullName,
                Created = reply.Created,
                PostId = reply.Post.Id
            });

        }
    }
}
