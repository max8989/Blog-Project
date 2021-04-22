using Blog_Project.Data;
using Blog_Project.ViewModels;
using Blog_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog_Project.Data.Repository;

namespace Blog_Project.Components
{
    public class LayoutCategories : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        public LayoutCategories(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepository.AllCategories;
            return View(categories);
        }
    }
}
