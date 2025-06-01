using TripApp.Core.Models;

namespace TripApp.Application.Repository;

public interface IClientRepository
{
    Task<bool> ClientExistsAsync(int clientId);
    Task<bool> ClientHasTripsAsync(int clientId);
    Task DeleteClientAsync(int clientId);
    Task<Client?> GetClientByPeselAsync(string pesel);
    Task AddClientAsync(Client client);
}