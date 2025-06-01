using TripApp.Core.Models;

namespace TripApp.Application.Repository;

public interface ITripRepository
{
    Task<PaginatedResult<Trip>> GetPaginatedTripsAsync(int page = 1, int pageSize = 10);
    Task<List<Trip>> GetAllTripsAsync();
    Task<Trip?> GetTripByIdAsync(int tripId);
}