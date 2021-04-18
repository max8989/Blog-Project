using Blog_Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Blog_Project.Models
{
    public class Like
    {
        public int Id { get; set; }

        public Post Post { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


    }
}
