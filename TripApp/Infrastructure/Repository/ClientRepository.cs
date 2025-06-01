using Microsoft.EntityFrameworkCore;
using TripApp.Application.Repository;
using TripApp.Core.Models;

namespace TripApp.Infrastructure.Repository;

public class ClientRepository : IClientRepository
{
    private readonly TripContext _context;

    public ClientRepository(TripContext context)
    {
        _context = context;
    }

    public async Task<bool> ClientExistsAsync(int clientId)
    {
        return await _context.Clients.AnyAsync(c => c.IdClient == clientId);
    }

    public async Task<bool> ClientHasTripsAsync(int clientId)
    {
        return await _context.ClientTrips.AnyAsync(ct => ct.IdClient == clientId);
    }

    public async Task DeleteClientAsync(int clientId)
    {
        var client = await _context.Clients.FindAsync(clientId);
        if (client == null) return;

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }

    public async Task<Client?> GetClientByPeselAsync(string pesel)
    {
        return await _context.Clients.SingleOrDefaultAsync(c => c.Pesel == pesel);
    }

    public async Task AddClientAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
    }
}