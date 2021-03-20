using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLL.DTOs
{
    public class UserDTO
    {

        public string Id { get; set; }
        [Required, MaxLength(50), MinLength(2)]
        public string FirstName { get; set; }
        [Required, MaxLength(50), MinLength(2)]
        public string LastName { get; set; }
        [Required, MaxLength(50), MinLength(2)]
        public string UserName { get; set; }
        [Required, MaxLength(50), MinLength(9)]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }
        [Required, MaxLength(50), MinLength(6)]

        public string Password { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal Balance { get; set; }
    }
}
