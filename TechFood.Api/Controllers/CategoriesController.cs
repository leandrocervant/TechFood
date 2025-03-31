using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Categories.Queries.ListCategories;

namespace TechFood.Api.Controllers
{
    [ApiController()]
    [Route("v1/categories")]
    public class CategoriesController(ISender mediator) : ControllerBase
    {
        private readonly ISender _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> ListCategoriesAsync()
        {
            var query = new ListCategoriesQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
