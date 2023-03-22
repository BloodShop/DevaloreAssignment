using System.ComponentModel.DataAnnotations;

namespace DevaloreAssignment.AppSettingsOptions
{
    public class UserApiOptions // Options Pattern https://www.youtube.com/watch?v=SizJCLcjbOA&list=PL59L9XrzUa-nqfCHIKazYMFRKapPNI4sP&index=16&ab_channel=RahulNath
    {
        public const string UserApi = "UserApi";

        [Required]
        public string Name { get; set; }

        [Required]
        public string BaseAddress { get; set; }
    }
}
