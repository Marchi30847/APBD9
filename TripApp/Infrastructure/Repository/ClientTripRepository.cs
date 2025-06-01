using Microsoft.EntityFrameworkCore;
using TripApp.Application.Repository;
using TripApp.Core.Models;

namespace TripApp.Infrastructure.Repository
{
    public class ClientTripRepository : IClientTripRepository
    {
        private readonly TripContext _context;

        public ClientTripRepository(TripContext context)
        {
            _context = context;
        }

        public async Task<bool> IsClientRegisteredToTripAsync(int clientId, int tripId)
        {
            return await _context.ClientTrips
                .AnyAsync(ct => ct.IdClient == clientId && ct.IdTrip == tripId);
        }

        public async Task AddClientToTripAsync(int clientId, int tripId, DateTime registeredAt, DateTime? paymentDate)
        {
            var clientTrip = new ClientTrip
            {
                IdClient = clientId,
                IdTrip = tripId,
                RegisteredAt = registeredAt,
                PaymentDate = paymentDate
            };

            await _context.ClientTrips.AddAsync(clientTrip);
            await _context.SaveChangesAsync();
        }
    }
}