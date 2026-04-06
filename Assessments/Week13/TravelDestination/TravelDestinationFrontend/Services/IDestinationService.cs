using TravelDestinationFrontend.Models;

namespace TravelDestinationFrontend.Services
{
   
        public interface IDestinationService
        {
        Task<IEnumerable<DestinationViewModel>> GetAllAsync();
        Task<DestinationViewModel?> GetByIdAsync(int id);
        Task<bool> CreateAsync(DestinationViewModel destination);
        Task<bool> UpdateAsync(DestinationViewModel destination);
        Task<bool> DeleteAsync(int id);
    }
    }

