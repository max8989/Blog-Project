using Blog_Project.ViewModels;
using Personal_Blog_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project.Data.Repository
{
    public interface IPostRepository
    {
        Post GetPost(int id);
        Task<IEnumerable<Post>> GetAllPosts();
        Post AddPost();
        Post UpdatePost(int id);
        void RemovePost(int id);
    }
}
