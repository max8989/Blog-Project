using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blog_Project.Data;
using Blog_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Blog_Project.ViewModels;
using Blog_Project.Data.Repository;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Blog_Project.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPostRepository _postRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICategoryRepository _categoryRepository;

        public PostsController(ApplicationDbContext context, IPostRepository postRepository,
                                                    UserManager<ApplicationUser> userManager,
                                                    ICategoryRepository categoryRepository)
        {
            _context = context;
            _postRepository = postRepository;
            _userManager = userManager;
            _categoryRepository = categoryRepository;
        }

        // USER ID
        //var currentUser = await _userManager.GetUserAsync(HttpContext.User);
        //currentUser.Id;

        // DB Post
        // _context.Posts

        //=========================================================================================================================================//


        // Danik / Marc
        // GET: Posts for current user IMPLEMENT
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            //if admin show all posts
            // else show for current User
            var applicationDbContext = _context.Posts
                .Include(c => c.Category)
                .Include(l => l.Likes)
                .Include(cc => cc.mainComments)
                .ThenInclude(ccc => ccc.SubComments)
                .Where(u => u.UserId == currentUser.Id);

            return View(await applicationDbContext.ToListAsync());
        }

        // Danik / Marc
        // GET: Posts for current user IMPLEMENT
        public async Task<IActionResult> IndexByUser(string userId)
        {
            //var postViewModel = new PostViewModel
            //{
            //    Posts = await _postRepository.GetAllPosts()
            //};
            //return View(postViewModel);
           

            var applicationDbContext = _context.Posts
                .Include(c => c.Category)
                .Include(l => l.Likes)
                .Include(cc => cc.mainComments)
                .ThenInclude(ccc => ccc.SubComments);

            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Get post by id
            var post = await _context.Posts
                .Include(p => p.mainComments)
                .ThenInclude(p=> p.SubComments)
                .Include(p => p.Likes)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            var postViewModel = new PostViewModel();

            if(post != null)
            {
                List<CommentViewModel> comments = new List<CommentViewModel>();
                foreach (var comment in post.mainComments)
                {
                    var firstName = _context.Users.Find(comment.UserId)?.FirstName;
                    var lastName = _context.Users.Find(comment.UserId)?.LastName;
                    var commentViewModel = new CommentViewModel
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Id = comment.Id,
                        Message = comment.Message,
                        UserId = comment.UserId,
                        DateCreated = comment.DateCreated,
                        PostId = comment.PostId,
                        Post = comment.Post,
                        SubComments = comment.SubComments
                    };

                    comments.Add(commentViewModel);
                }

                postViewModel = new PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Description = post.Description,
                    Body = post.Body,
                    Image = post.Image,
                    CategoryId = post.CategoryId,
                    Category = post.Category,
                    DateCreated = post.DateCreated,
                    Likes = post.Likes,
                    mainComments = comments
                };
            }
            if (post.UserId != null)
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                var firstName = _context.Users.Find(post.UserId)?.FirstName;
                var lastName = _context.Users.Find(post.UserId)?.LastName;
               
                if(firstName != null && lastName != null)
                {
                    postViewModel.FirstName = firstName;
                    postViewModel.LastName = lastName;
                }
            }
            return View(postViewModel);
        }

        [Authorize]
        // GET: Posts/Create
        public async Task<IActionResult> Create()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var postViewModel = new PostViewModel();
            postViewModel.Categories = new SelectList(_categoryRepository.AllCategories, "CategoryId", "CategoryName");
            postViewModel.UserId = currentUser.Id;
            return View(postViewModel);
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel postViewModel, IFormFile? formFile)
        {
            if (ModelState.IsValid)
            {

                if(formFile != null)
                {
                    // Save img in Base64 format
                    byte[] bytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        formFile.CopyTo(memoryStream);
                        bytes = memoryStream.ToArray();
                    }

                    string base64Img = Convert.ToBase64String(bytes);
                    postViewModel.Image = base64Img;
                }


                var post = new Post
                {
                    Title = postViewModel.Title,
                    Description = postViewModel.Description,
                    Body = postViewModel.Body,
                    DateCreated = DateTime.Now,
                    CategoryId = postViewModel.CategoryId,
                    UserId = postViewModel.UserId,
                    Image = postViewModel.Image
                };

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postViewModel);
        }


        [Authorize]
        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var postViewModel = new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Body = post.Body,
                CategoryId = post.CategoryId,
                UserId = post.UserId,
                Image = post.Image,
                Categories = new SelectList(_categoryRepository.AllCategories, "CategoryId", "CategoryName")
        };



            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", post.CategoryId);
            return View(postViewModel);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var post = await _context.Posts.FindAsync(postViewModel.Id);
                    if (post == null)
                    {
                        return NotFound();
                    }

                    post.Title = postViewModel.Title;
                    post.Description = postViewModel.Description;
                    post.Body = postViewModel.Body;
                    post.CategoryId = postViewModel.CategoryId;
                    post.Image = postViewModel.Image;

                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(postViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", post.CategoryId);
            return View(postViewModel);
        }

        [Authorize]
        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }

        // IMPLEMENT Olivier
        // -1 => error, 0 => liked, 1 => unliked, 
        public async Task<int> toggleLikeAsync(string userId, Post post, bool isLiked)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (isLiked)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        // IMPLEMENT Justin
        [Authorize]
        public IActionResult addComment(int postId, string commentBody)
        {
            var comment = new
            {
                postId = postId,
                commentBody = commentBody
            };

            if (ModelState.IsValid)
            {
                var Newcomment = new MainComment
                {
                    Message = commentBody,
                    UserId = _userManager.GetUserId(HttpContext.User),
                    DateCreated = DateTime.Now,
                    PostId = postId
                };

                _context.Add(Newcomment);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Details", postId);
        }

        //public async Task<Comment[]> GetAllCommentAsync(bool includeMainComments = false)
        //{
        //    _logger.LogInformation($"Getting all comments");

        //    IQueryable<Comment> query = _context.Comment
        //        .Include(c => c.Post);

        //    if (includeMainComments)
        //    {
        //        query = query
        //          .Include(c => c.mainComments)
        //          .ThenInclude(t => t.subComments);
        //    }

        //    // Order It
        //    query = query.OrderByDescending(c => c.EventDate);

        //    return await query.ToArrayAsync();
        //}


    }
}
