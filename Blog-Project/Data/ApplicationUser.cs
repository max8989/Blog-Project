using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string LastName { get; set; }

    }
}
