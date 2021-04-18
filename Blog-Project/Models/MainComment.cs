using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Blog_Project.Models
{
    public class MainComment : Comment
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public List<SubComment> SubComments { get; set; }

    }
}
