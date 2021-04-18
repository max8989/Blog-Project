using Personal_Blog_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project.Data.Repository
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();
        void AddPost();
        void UpdatePost();
        void RemovePost();
        void AddSubComment(SubComment comment);
    }
}
