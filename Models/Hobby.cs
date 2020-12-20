using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class Hobby
    {
        [Key]
        public int HobbyId {get; set;}

        public int UserId {get; set;}
        public User User {get; set;}

        public int FunId {get; set;}
        public TheFun FunHobby {get; set;} 
    }
}
