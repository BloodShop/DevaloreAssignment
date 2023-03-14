using DevaloreAssignment.Dto;
using DevaloreAssignment.Models;

namespace DevaloreAssignment.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersData(string gender, int amount = 10);
        Task<string> GetMostPopularCountry();
        Task<IEnumerable<string>> GetListOfMails();
        Task<UserDto> GetOldestUser();

        Task<bool> CreateNewUser(User createUserDto);
        Task<CreateUserDto> GetNewUser();
        Task<bool> UpdateUserData(User updatedUser);
        Task<User?> GetUserByEmail(string email);
    }
}