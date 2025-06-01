using Microsoft.EntityFrameworkCore;
using TripApp.Application.Repository;
using TripApp.Core.Models;

namespace TripApp.Infrastructure.Repository;

public class TripRepository : ITripRepository
{
    private readonly TripContext _context;

    public TripRepository(TripContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResult<Trip>> GetPaginatedTripsAsync(int page = 1, int pageSize = 10)
    {
        var query = _context.Trips
            .Include(t => t.IdCountries)
            .Include(t => t.ClientTrips).ThenInclude(ct => ct.IdClientNavigation)
            .OrderByDescending(t => t.DateFrom)
            .AsNoTracking();

        var totalCount = await query.CountAsync();
        var allPages = (int)System.Math.Ceiling(totalCount / (double)pageSize);

        var trips = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResult<Trip>
        {
            PageNum = page,
            PageSize = pageSize,
            AllPages = allPages,
            Data = trips
        };
    }

    public async Task<List<Trip>> GetAllTripsAsync()
    {
        return await _context.Trips
            .Include(t => t.IdCountries)
            .Include(t => t.ClientTrips).ThenInclude(ct => ct.IdClientNavigation)
            .OrderByDescending(t => t.DateFrom)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Trip?> GetTripByIdAsync(int tripId)
    {
        return await _context.Trips
            .Include(t => t.ClientTrips)
            .SingleOrDefaultAsync(t => t.IdTrip == tripId);
    }
}