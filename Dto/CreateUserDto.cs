using DevaloreAssignment.Models;

namespace DevaloreAssignment.Dto
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }

    }
}
