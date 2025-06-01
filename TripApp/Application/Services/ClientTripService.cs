using TripApp.Application.DTOs;
using TripApp.Application.Exceptions;
using TripApp.Application.Repository;
using TripApp.Application.Services.Interfaces;
using TripApp.Core.Models;

namespace TripApp.Application.Services;

public class ClientTripService : IClientTripService
{
    private readonly IClientRepository _clientRepository;
    private readonly ITripRepository _tripRepository;
    private readonly IClientTripRepository _clientTripRepository;

    public ClientTripService(
        IClientRepository clientRepository,
        ITripRepository tripRepository,
        IClientTripRepository clientTripRepository)
    {
        _clientRepository = clientRepository;
        _tripRepository = tripRepository;
        _clientTripRepository = clientTripRepository;
    }

    public async Task<int> RegisterClientToTripAsync(int tripId, RegisterClientRequestDto dto)
    {
        var existingClient = await _clientRepository.GetClientByPeselAsync(dto.Pesel);
        if (existingClient != null)
            throw new ClientExceptions.ClientPeselAlreadyExistsException();

        var trip = await _tripRepository.GetTripByIdAsync(tripId);
        if (trip == null)
            throw new TripExceptions.TripNotFoundException();

        if (trip.DateFrom <= DateTime.UtcNow)
            throw new TripExceptions.TripHasStartedException();

        var client = new Client
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Telephone = dto.Telephone,
            Pesel = dto.Pesel
        };

        await _clientRepository.AddClientAsync(client);
        int newClientId = client.IdClient;

        bool alreadyRegistered = await _clientTripRepository.IsClientRegisteredToTripAsync(newClientId, tripId);
        if (alreadyRegistered)
            throw new ClientExceptions.ClientAlreadyRegisteredException();

        DateTime registeredAt = DateTime.UtcNow;
        DateTime? paymentDate = dto.PaymentDate;

        await _clientTripRepository.AddClientToTripAsync(newClientId, tripId, registeredAt, paymentDate);

        return newClientId;
    }
}