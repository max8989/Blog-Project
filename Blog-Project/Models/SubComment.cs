﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project.Models
{
    public class SubComment : Comment
    {
        public int MainCommentId { get; set; }

    }
}
