using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
namespace FleetManagement.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AuthService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<string> GetGpsData(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(
                _config["ApiSettings:BaseUrl"] + "tracking/gps"
            );

            if (!response.IsSuccessStatusCode)
                return "Access Denied";

            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> Login(string email, string password)
        {
            var data = new
            {
                Email = email,
                Password = password
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(data),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(
                _config["ApiSettings:BaseUrl"] + "auth/login",
                content
            );

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(json);

            return result.access_token;
        }
    }
}