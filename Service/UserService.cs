using DevaloreAssignment.Dto;
using DevaloreAssignment.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace DevaloreAssignment.Services
{
    public class UserService : IUserService
    {
        //private const string ApiKey = "";
        private readonly IHttpClientFactory _httpClientFactory;
        private static ICollection<User> _users = new List<User>();

        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Task<bool> CreateNewUser(User createUserDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable <string>> GetListOfMails()
        {
            var client = _httpClientFactory.CreateClient("usersapi");
            var response = await client.GetAsync($"?results=30");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var results = JsonConvert.DeserializeObject<ResultResponse>(result);

                return results.users.Select(g => g.email);
            }

            return null;
        }

        public async Task<string?> GetMostPopularCountry()
        {
            var client = _httpClientFactory.CreateClient("usersapi");
            var response = await client.GetAsync($"?results=5000");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var results = JsonConvert.DeserializeObject<ResultResponse>(result);

                return results.users.GroupBy(x => x.location.country)
                               .OrderByDescending(g => g.Count())
                               .Select(g => g.Key)
                               .FirstOrDefault();
            }

            return null;
        }

        public async Task<CreateUserDto> GetNewUser()
        {
            
            return null;
        }

        public async Task<UserDto?> GetOldestUser()
        {
            var client = _httpClientFactory.CreateClient("usersapi");
            var response = await client.GetAsync($"?results=100");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var results = JsonConvert.DeserializeObject<ResultResponse>(result);

                var oldest = results.users.OrderByDescending(g => g.dob.age).FirstOrDefault();

                return new UserDto() { name = oldest.name, age = oldest.dob.age };
            }

            return null;
        }

        public async Task<IEnumerable<User>> GetUsersData(string gender, int amount = 10)
        {
            var client = _httpClientFactory.CreateClient("usersapi");
            var response = await client.GetAsync($"?gender={gender}&results={amount}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var resultJson = JsonConvert.DeserializeObject<ResultResponse>(result);

                return resultJson.users;
            }

            return Enumerable.Empty<User>();
        }

        public async Task<bool> UpdateUserData(User updatedUser)
        {
            var existingUser = await GetUserByEmail(updatedUser.email);
            if (existingUser != null)
            {
                return false;
            }

            _users.Remove(existingUser);
            _users.Add(updatedUser);
            return true;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var client = _httpClientFactory.CreateClient("usersapi");
            var response = await client.GetAsync($"?email={email}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var resultJson = JsonConvert.DeserializeObject<ResultResponse>(result);

                return resultJson?.users?[0];
            }

            return null;
        }
    }
}
