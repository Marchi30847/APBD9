using Microsoft.AspNetCore.Mvc;
using TripApp.Application.Exceptions;
using TripApp.Application.Services.Interfaces;

namespace TripApp.Presentation.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpDelete("{clientId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteClient(int clientId)
    {
        try
        {
            bool removed = await _clientService.DeleteClientAsync(clientId);
            if (!removed)
            {
                return NotFound(new { message = $"Client with Id = {clientId} not found." });
            }

            return NoContent();
        }
        catch (ClientExceptions.ClientHasTripsException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}