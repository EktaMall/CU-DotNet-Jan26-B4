using Microsoft.EntityFrameworkCore;
using TravelDestinationApi.Data;
using TravelDestinationApi.Exceptions;
using TravelDestinationApi.Models;

namespace TravelDestinationApi.Repository
{
    public class DestinationRepository : IDestinationRepository
    {
        private readonly AppDbContext _context;

        public DestinationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Destination>> GetAllAsync()
        {
            return await _context.Destinations
                .OrderBy(d => d.CityName)
                .ToListAsync();
        }

        public async Task<Destination> GetByIdAsync(int id)
        {
            var destination = await _context.Destinations.FindAsync(id);

            if (destination == null)
            {
                throw new DestinationNotFoundException($"Destination with ID {id} was not found.");
            }

            return destination;
        }

        public async Task AddAsync(Destination destination)
        {
            if (destination.Rating < 1 || destination.Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }

            _context.Destinations.Add(destination);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Destination destination)
        {
            var existing = await _context.Destinations.FindAsync(destination.Id);

            if (existing == null)
            {
                throw new DestinationNotFoundException($"Destination with ID {destination.Id} was not found.");
            }

            if (destination.Rating < 1 || destination.Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }

            existing.CityName = destination.CityName;
            existing.Country = destination.Country;
            existing.Description = destination.Description;
            existing.Rating = destination.Rating;
            existing.LastVisited = destination.LastVisited;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var destination = await _context.Destinations.FindAsync(id);

            if (destination == null)
            {
                throw new DestinationNotFoundException($"Destination with ID {id} was not found.");
            }

            _context.Destinations.Remove(destination);
            await _context.SaveChangesAsync();
        }
    }
}