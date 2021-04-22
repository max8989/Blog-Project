using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Personal_Blog_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project.Data.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public CommentRepository(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        public async Task<string> addComment(string userId, int postId)
        {
          // var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            
            throw new NotImplementedException();
        }

        public bool addMainComments(int postId)
        {
            throw new NotImplementedException();
        }

        public bool addSubComments(MainComment mainComment)
        {
            throw new NotImplementedException();
        }
    }
}
