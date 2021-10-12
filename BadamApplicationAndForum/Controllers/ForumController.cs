using BadamApplicationAndForum.Data.Dtos;
using BadamApplicationAndForum.Data.Dtos.Forums;
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
    [Route("api/app/forums")]
    public class ForumController : Controller
    {
        private readonly IForum _forumService;
        private readonly IPost _postService;


        public ForumController(IForum forumService, IPost postService)
        {
            _forumService = forumService;
            _postService = postService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = MVSJwtTokens.AuthScheme)]
        public ActionResult GetForum()
        {
            var forums = _forumService.GetAllForums().Select(forum => new ForumListingModel
            {
                Id = forum.Id,
                Title = forum.Title,
                Description = forum.Description
            });
            var model = new ForumIndexModel
            {
                ForumList = forums
            };
            return Ok(model);
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = MVSJwtTokens.AuthScheme)]
        public ActionResult Topic(int id)
        {
            var forum = _forumService.GetForumById(id);
            var posts = _postService.GetPostsByForum(id);
            if (posts == null || forum == null)
            {
                return NotFound();
            }

            var postListings = posts.Select(p => new PostListingModel
            {
                Id = p.Id,
                AuthorName = p.ApplicationUser.UserName,
                AuthorId = p.ApplicationUser.Id.ToString(),
                DatePosted = p.Created,
                RepliesCount = p.PostReplies.Count(),
                Title = p.Title,
                Content = p.Content,
                Replies = BuildReplies(p),
                Forum = BuildForumListing(p)
            });
            var model = new ForumTopicModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forum)
            };
            return Ok(model);
        }

        private IEnumerable<PostReplyModel> BuildReplies(Post p)
        {
            var replies = p.PostReplies.Select(r => new PostReplyModel
            {
                PostId = p.Id,
                Id = r.Id,
                AuthorId = r.ApplicationUser.Id.ToString(),
                AuthorName = r.ApplicationUser.UserName,
                Content = r.Content,
                Created = r.Created
            });
            return replies;
        }

        private static ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;
            return BuildForumListing(forum);
        }
        private static ForumListingModel BuildForumListing(Forum forum)
        {
            return new ForumListingModel
            {
                Id = forum.Id,
                Title = forum.Title,
                Description = forum.Description
            };
        }
    }
}
