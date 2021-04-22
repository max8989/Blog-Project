using Personal_Blog_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project.Data.Repository
{
    public interface ICommentRepository
    {
        bool addMainComments(int postId);
        bool addSubComments(MainComment mainComment);
        //string addComment(string userId, int postId);
    }
}
