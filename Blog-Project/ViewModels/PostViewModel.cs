using Personal_Blog_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project.ViewModels
{
    public class PostViewModel
    {
        public IEnumerable<Post> Posts { get; set; }

    }
}
