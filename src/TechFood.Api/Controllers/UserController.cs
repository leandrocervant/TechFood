using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechFood.Application.Users.Queries.GetUserById;

namespace TechFood.Api.Controllers
{
    [ApiController()]
    [Route("v1/user")]
    public class UserController(ISender mediator) : ControllerBase
    {
        private readonly ISender _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetUserByIdAsync()
        {
            var query = new GetUserByIdQuery(User.GetUserId());
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
