using MediatR;
using System.Collections.Generic;
using TechFood.Application.Categories.Models;

namespace TechFood.Application.Categories.Queries.ListCategories
{
    public record ListCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;
}
