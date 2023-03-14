namespace DevaloreAssignment.Dto
{
    public class CreateUserDto
    {


        public string name { get; set; }
        public int age { get; set; }
        public string email { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string phone { get; set; }
        public string country { get; set; }

        public void CreateUser()
        {

        }
    }
}
