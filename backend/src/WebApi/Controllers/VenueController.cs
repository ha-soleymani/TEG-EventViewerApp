using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VenueController(GetVenues useCase) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await useCase.ExecuteAsync());
    }
}
