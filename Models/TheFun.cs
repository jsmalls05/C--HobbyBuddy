using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
public class TheFun
    {
    [Key]
    public int FunId {get; set;}
    //Foriegn key: Owner of the idea
    public int UserId {get; set;}

    [Required]
    [MinLength(3)]
    public string Name {get; set;}

    [Required]
    [MinLength(5)]
    public string Description {get; set;}
    
    public User Owner {get; set;}
    public List<Hobby> HobbyBy {get; set;}
    }
}