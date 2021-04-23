using Microsoft.AspNetCore.Mvc.Rendering;
using Blog_Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog_Project.Data;
using Blog_Project.Data.Repository;

namespace Blog_Project.ViewModels
{
    public class CommentViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public string Message { get; set; }
        public string? UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

        public List<SubComment> SubComments { get; set; }
    }
}
