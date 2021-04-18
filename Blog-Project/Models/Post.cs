using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Blog_Project.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string? Image { get; set; }
        public string Description { get; set; }
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Like> Likes { get; set; }

        public DateTime DateCreated { get; set; }

        public List<MainComment> mainComments { get; set; }

    }
}
