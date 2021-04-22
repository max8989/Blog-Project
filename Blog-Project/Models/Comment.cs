using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project.Models

{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }

        [ForeignKey("AspNetUsers")]
        [Column(TypeName = "NVARCHAR(450)")]
        public string? UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
