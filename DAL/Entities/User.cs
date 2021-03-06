using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class User : IdentityUser
    {
        [Required, MaxLength(50), MinLength(2)]
        public string FirstName { get; set; }
        [Required, MaxLength(50), MinLength(2)]
        public string LastName { get; set; }
        [Required, MaxLength(50), MinLength(9)]
        public string Phone { get; set; }
        public string Role { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
