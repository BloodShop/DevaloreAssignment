using System.ComponentModel.DataAnnotations;

namespace DevaloreAssignment.AppSettingsOptions
{
    public class MyCorsOptions
    {
        [Required]
        public string[] AllowedOrigins { get; set; }
    }
}
