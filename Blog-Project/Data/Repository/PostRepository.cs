using Personal_Blog_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blog_Project.ViewModels;

namespace Blog_Project.Data.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PostRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Post AddPost()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            var allPosts = _applicationDbContext.Posts
                .Include(c => c.Category)
                .Include(cc => cc.mainComments)
                .ThenInclude(ccc => ccc.SubComments);

            return (await allPosts.ToListAsync());
        }

        public Post GetPost(int id)
        {
            var result = _applicationDbContext.Posts
                .Include(c => c.Id)
                .Include(c => c.Category)
                .Include(cc => cc.mainComments)
                .ThenInclude(ccc => ccc.SubComments)
                .Where(c => c.Id == id);
            return (Post)result;
        }

        public void RemovePost(int id)
        {
            var result = _applicationDbContext.Posts
            .Include(c => c.Id).Where(c => c.Id == id);
            _applicationDbContext.Remove(result);
        }

        public Post UpdatePost(int id)
        {
            throw new NotImplementedException();
        }
    }
}
