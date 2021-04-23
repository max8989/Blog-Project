using Blog_Project.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project.Models
{
    public class Like
    {
        public int Id { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        [ForeignKey("AspNetUsers")]
        [Column(TypeName = "NVARCHAR(450)")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


    }
}
