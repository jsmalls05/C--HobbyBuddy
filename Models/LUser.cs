using System;
using System.ComponentModel.DataAnnotations;


namespace Exam.Models
{
    public class LUser
    {
        [Required]
        [MinLength(3)]
        public string LUserName {get; set;}

        [Required]
        [DataType(DataType.Password)]
        public string LPassword {get; set;}
    }
}