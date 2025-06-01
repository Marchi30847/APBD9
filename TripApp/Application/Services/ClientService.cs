using TripApp.Application.Repository;
using TripApp.Application.Services.Interfaces;
using TripApp.Application.Exceptions;

namespace TripApp.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<bool> DeleteClientAsync(int clientId)
        {
            bool exists = await _clientRepository.ClientExistsAsync(clientId);
            if (!exists)
                throw new ClientExceptions.ClientNotFoundException();

            bool hasTrips = await _clientRepository.ClientHasTripsAsync(clientId);
            if (hasTrips)
                throw new ClientExceptions.ClientHasTripsException();

            await _clientRepository.DeleteClientAsync(clientId);
            return true;
        }
    }
}