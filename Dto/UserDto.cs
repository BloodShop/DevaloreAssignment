using DevaloreAssignment.Models;
using System.ComponentModel.DataAnnotations;

namespace DevaloreAssignment.Dto
{
    public class UserDto
    {
        [Required]
        public Name name { get; set; }

        [Required]
        [Range(18, int.MaxValue)]
        public int age{ get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }
    }
}
