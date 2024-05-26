using Microsoft.AspNetCore.Mvc;
using tmp.ENUMs;
using TripApi.DTOs;
using TripApi.Services;

namespace TripApi.Controllers;

[ApiController]
public class TripController : ControllerBase
{
    private ITripService _tripService;
    
    public TripController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet("/api/trips")]
    public async Task<IActionResult> GetTrips()
    {
        return Ok(await _tripService.GetTrips());
    }
    
    [HttpDelete("/api/clients/{idClient:int}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        var result = await _tripService.DeleteClient(idClient);
        if (result == Errors.BadRequest)
            return BadRequest("Cannot delete client with assigned trips");
        if (result == Errors.NotFound)
            return StatusCode(StatusCodes.Status404NotFound);
        return Ok(result);
    }
    
    [HttpPost("/api/trips/{idTrip}/clients")]
    public async Task<ActionResult> AssignClientToTrip(ClientInputDTO clientInputDto)
    {
        var result = await _tripService.AssignClientToTrip(clientInputDto);
        if (result == Errors.BadRequest)
            return BadRequest(StatusCodes.Status400BadRequest);
        if (result == Errors.NotFound)
            return StatusCode(StatusCodes.Status404NotFound);
        return Ok();
    }
}