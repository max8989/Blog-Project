﻿using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class PostViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public string? Image { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime DateCreated { get; set; }
        //public string CategoryName { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
        public List<Like> Likes { get; set; }

        public List<CommentViewModel> mainComments { get; set; }

        public bool IsLiked { get; set; }
        //public IEnumerable<Post> Posts { get; set; }

    }
}
