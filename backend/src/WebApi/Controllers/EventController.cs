using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController(GetEventsByVenue useCase) : ControllerBase
{
    [HttpGet("{venueId}")]
    public async Task<IActionResult> Get(int venueId) =>
        Ok(await useCase.ExecuteAsync(venueId));

}