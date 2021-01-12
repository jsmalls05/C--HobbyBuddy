using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam.Models
{
    public class User
    {
        [Key]
        public int UserId {get; set;}

        [Required]
        [MinLength(3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage="Your First name must only contain letters")] 
        public string FirstName {get; set;}

        [Required]
        [MinLength(3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage="Your Last name must only contain letters")]
        public string LastName {get; set;}

        [Required]
        [MinLength(3)]
        public string UserName {get; set;}

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password {get; set;}
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<Hobby> UserHobby {get; set;}
        public List<TheFun> UsersFun {get; set;}
        
        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Confirm {get; set;}
    }
}
