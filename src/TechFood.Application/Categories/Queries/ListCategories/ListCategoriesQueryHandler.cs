using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechFood.Application.Categories.Models;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;

namespace TechFood.Application.Categories.Queries.ListCategories
{
    public class ListCategoriesQueryHandler(
        IMapper mapper,
        ICategoryRepository categoryRepository,
        IConfiguration appConfiguration) : IRequestHandler<ListCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IConfiguration _appConfiguration = appConfiguration;

        public async Task<IEnumerable<CategoryDto>> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories
                .Select(category => _mapper.Map<Category, CategoryDto>(
                    category,
                    options => options.AfterMap((category, dto) =>
                    {
                        dto.ImageUrl = string.Concat(
                            _appConfiguration["TechFoodStaticImagesUrl"],
                            "/discoveries/",
                            category.ImageFileName);
                    })));
        }
    }
}
