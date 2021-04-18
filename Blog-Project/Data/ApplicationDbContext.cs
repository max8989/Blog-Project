using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Personal_Blog_Project.Models;
using Blog_Project.Data.Repository;

namespace Blog_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IDateTimeNow _dateTimeNow;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeNow dateTimeNow)
            : base(options)
        {
            _dateTimeNow = dateTimeNow;
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MainComment> MainComments { get; set; }
        public DbSet<SubComment> SubComments { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed Categories
            builder.Entity<Category>().HasData(new Category { CategoryId = 1, CategoryName = "DotNet Core" });
            builder.Entity<Category>().HasData(new Category { CategoryId = 2, CategoryName = "BlockChain" });
            builder.Entity<Category>().HasData(new Category { CategoryId = 3, CategoryName = "La Cite" });

            // Seed MainComments
            builder.Entity<MainComment>().HasData(new MainComment 
            {
                Id = 1,
                Message = "Super bon Post!!!",
                DateCreated = _dateTimeNow.GetDateTimeNow(),
                PostId = 1
            });

            builder.Entity<MainComment>().HasData(new MainComment
            {
                Id = 2,
                Message = "Cest mauvais",
                DateCreated = _dateTimeNow.GetDateTimeNow(),
                PostId = 1
            });
            builder.Entity<MainComment>().HasData(new MainComment
            {
                Id = 3,
                Message = "Cest le meilleur de toute ma vie!!!!!",
                DateCreated = _dateTimeNow.GetDateTimeNow(),
                PostId = 2
            });


            // Seed SubComments
            builder.Entity<SubComment>().HasData(new SubComment
            {
                Id = 1,
                MainCommentId = 1,
                Message = "Non je ne penses pas...",
                DateCreated = _dateTimeNow.GetDateTimeNow()
            });

            builder.Entity<SubComment>().HasData(new SubComment
            {
                Id = 2,
                MainCommentId = 3,
                Message = "Oui 100% daccord",
                DateCreated = _dateTimeNow.GetDateTimeNow()       
            });



            // Seed Post
            builder.Entity<Post>().HasData(new Post 
            {
                Id = 1,
                Title = "Le rechaufement planetaire",
                Body = "It is a long established fact that a reader will be distracted" +
                " by the readable content of a page when looking at its layout." +
                " The point of using Lorem Ipsum is that it has a more-or-less normal" +
                " distribution of letters, as opposed to using 'Content here, content here',",
                Description = "Description 1",
                CategoryId = 1,
                DateCreated = _dateTimeNow.GetDateTimeNow(),
                mainComments = new List<MainComment>()
                //{
                //    new MainComment {Id = 1},
                //    new MainComment {Id = 2}
                //}
            });

            builder.Entity<Post>().HasData(new Post
            {
                Id = 2,
                Title = "Le Covid 19",
                Body = "It is a long established fact that a reader will be distracted" +
                " by the readable content of a page when looking at its layout." +
                " The point of using Lorem Ipsum is that it has a more-or-less normal" +
                " distribution of letters, as opposed to using 'Content here, content here',",
                Description = "Description 2",
                CategoryId = 2,
                DateCreated = _dateTimeNow.GetDateTimeNow(),
                //mainComments = new List<MainComment>()
                //{
                //    new MainComment {Id = 3}
                //}
            });



        }
    }
}
