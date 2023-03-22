using System.ComponentModel.DataAnnotations;

namespace DevaloreAssignment.Dto
{
    public class CreateUserDto : IValidatableObject
    {
        [Required]
        public string name { get; set; }

        [Required]
        [Range(18, int.MaxValue)]
        public int age { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime dateOfBirth { get; set; }

        [Phone]
        public string phone { get; set; }

        public string country { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(email))
            {
                yield return new ValidationResult(
                    "Either Phone or email must be specified",
                    new[] { nameof(phone), nameof(email) });
            }
        }
    }
}
