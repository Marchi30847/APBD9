using TripApp.Application.DTOs;

namespace TripApp.Application.Services.Interfaces;

public interface IClientTripService
{
    Task<int> RegisterClientToTripAsync(int tripId, RegisterClientRequestDto dto);
}