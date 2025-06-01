using TripApp.Application.DTOs;
using TripApp.Core.Models;

namespace TripApp.Application.Mappers;

public static class ClientMapper
{
    public static ClientDto MapToClientDto(this Client client)
    {
        return new ClientDto
        {
            FirstName = client.FirstName,
            LastName = client.LastName
        };
    }
}