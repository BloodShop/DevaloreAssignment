using DevaloreAssignment.Dto;
using DevaloreAssignment.Models;

namespace WeatherApiHttp.Clients
{
    public interface IUserClient
    {
        Task<IEnumerable<UserResponse>> GetUsersData(string gender, int amount = 10);
        Task<string> GetMostPopularCountry();
        Task<IEnumerable<string>> GetListOfMails();
        Task<UserDto> GetOldestUser();
    }
}