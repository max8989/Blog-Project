using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blog_Project.Data;
using Personal_Blog_Project.Models;
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

        public PostsController(ApplicationDbContext context, IPostRepository postRepository, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _postRepository = postRepository;
            _userManager = userManager;

        }

        // GET: Posts
        public async Task<IActionResult> Index()
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

            var post = await _context.Posts
                .Include(p => p.mainComments)
                .ThenInclude(p=> p.SubComments)
                .Include(p => p.Likes)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [Authorize]
        // GET: Posts/Create
        public async Task<IActionResult> Create()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var postViewModel = new PostViewModel();
            postViewModel.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
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
                    UserId = postViewModel.UserId
                };

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", post.CategoryId);
            return View(postViewModel);
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Title,Body,Image,Description,CategoryId,DateCreated")] Post post)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(post);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", post.CategoryId);
        //    return View(post);
        //}

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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", post.CategoryId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,Image,Description,CategoryId,DateCreated")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", post.CategoryId);
            return View(post);
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
    }
}
