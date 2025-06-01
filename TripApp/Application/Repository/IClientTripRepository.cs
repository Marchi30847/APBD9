namespace TripApp.Application.Repository;

public interface IClientTripRepository
{
    Task<bool> IsClientRegisteredToTripAsync(int clientId, int tripId);
    Task AddClientToTripAsync(int clientId, int tripId, DateTime registeredAt, DateTime? paymentDate);
}