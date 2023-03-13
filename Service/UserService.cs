using DevaloreAssignment.Dto;
using DevaloreAssignment.Models;
using Newtonsoft.Json;

namespace WeatherApiHttp.Clients
{
    public class UserClient : IUserClient
    {
        //private const string ApiKey = "";
        private readonly IHttpClientFactory _httpClientFactory;

        public UserClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable <string>> GetListOfMails()
        {
            var client = _httpClientFactory.CreateClient("usersapi");
            var response = await client.GetAsync($"?results=30");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var results = JsonConvert.DeserializeObject<ResultResponse>(result);

                return results.results.Select(g => g.email);
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

                return results.results.GroupBy(x => x.location.country)
                               .OrderByDescending(g => g.Count())
                               .Select(g => g.Key)
                               .FirstOrDefault();
            }

            return null;
        }

        public async Task<UserDto> GetOldestUser()
        {
            var client = _httpClientFactory.CreateClient("usersapi");
            var response = await client.GetAsync($"?results=100");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var results = JsonConvert.DeserializeObject<ResultResponse>(result);

                var oldest = results.results.OrderByDescending(g => g.dob.age).FirstOrDefault();

                return new UserDto() { name = oldest.name, age = oldest.dob.age };
            }

            return null;
        }

        public async Task<IEnumerable<UserResponse>> GetUsersData(string gender, int amount = 10)
        {
            var client = _httpClientFactory.CreateClient("usersapi");
            var response = await client.GetAsync($"?gender={gender}&results={amount}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var resultJson = JsonConvert.DeserializeObject<ResultResponse>(result);

                return resultJson.results;
            }

            return Enumerable.Empty<UserResponse>();
        }
    }
}
