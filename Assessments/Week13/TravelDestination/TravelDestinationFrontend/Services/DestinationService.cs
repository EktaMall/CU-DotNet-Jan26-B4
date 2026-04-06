using System.Net.Http.Json;
using TravelDestinationFrontend.Models;

namespace TravelDestinationFrontend.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly HttpClient _httpClient;

        public DestinationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<DestinationViewModel>> GetAllAsync()
        {
            try
            {
                var destinations = await _httpClient.GetFromJsonAsync<IEnumerable<DestinationViewModel>>("api/Destinations");
                return destinations ?? Enumerable.Empty<DestinationViewModel>();
            }
            catch
            {
                return Enumerable.Empty<DestinationViewModel>();
            }
        }

        public async Task<DestinationViewModel?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<DestinationViewModel>($"api/Destinations/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateAsync(DestinationViewModel destination)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Destinations", destination);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(DestinationViewModel destination)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Destinations/{destination.Id}", destination);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Destinations/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}