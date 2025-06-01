using Microsoft.AspNetCore.Mvc;
using TripApp.Application.DTOs;
using TripApp.Application.Exceptions;
using TripApp.Application.Services.Interfaces;

namespace TripApp.Presentation.Controllers;

[ApiController]
[Route("api/trips")]
public class TripController : ControllerBase
{
    private readonly ITripService _tripService;
    private readonly IClientTripService _clientTripService;

    public TripController(
        ITripService tripService,
        IClientTripService clientTripService)
    {
        _tripService = tripService;
        _clientTripService = clientTripService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTrips(
        [FromQuery(Name = "page")] int? page,
        [FromQuery(Name = "pageSize")] int? pageSize)
    {
        int actualPage = page ?? 1;
        int actualPageSize = pageSize ?? 10;

        var paginatedResult = await _tripService.GetPaginatedTripsAsync(actualPage, actualPageSize);

        var response = new
        {
            pageNum = paginatedResult.PageNum,
            pageSize = paginatedResult.PageSize,
            allPages = paginatedResult.AllPages,
            trips = paginatedResult.Data
        };

        return Ok(response);
    }

    [HttpPost("{tripId}/clients")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RegisterClientToTrip(int tripId, [FromBody] RegisterClientRequestDto dto)
    {
        try
        {
            int newClientId = await _clientTripService.RegisterClientToTripAsync(tripId, dto);

            return Created($"/api/clients/{newClientId}", new { clientId = newClientId, tripId = tripId });
        }
        catch (ClientExceptions.ClientPeselAlreadyExistsException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ClientExceptions.ClientAlreadyRegisteredException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (TripExceptions.TripNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (TripExceptions.TripHasStartedException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}