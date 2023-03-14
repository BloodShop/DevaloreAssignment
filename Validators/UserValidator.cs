using DevaloreAssignment.Dto;
using DevaloreAssignment.Models;
using FluentValidation;

namespace DevaloreAssignment.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.name).NotNull().NotEmpty();
        }
    }

    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(u => u.email).NotNull().NotEmpty();
        }
    }
}
