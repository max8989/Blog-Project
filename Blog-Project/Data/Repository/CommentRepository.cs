﻿using Personal_Blog_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project.Data.Repository
{
    public class CommentRepository : ICommentRepository
    {
        public bool addMainComments(int postId)
        {
            throw new NotImplementedException();
        }

        public bool addSubComments(MainComment mainComment)
        {
            throw new NotImplementedException();
        }
    }
}