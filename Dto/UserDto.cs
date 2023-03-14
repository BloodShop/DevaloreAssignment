using DevaloreAssignment.Models;
using System.ComponentModel.DataAnnotations;

namespace DevaloreAssignment.Dto
{
    public class UserDto
    {
        [Required] 
        public Name name { get; set; }
        public int age{ get; set; }
        public string email { get; set; }
    }
}
