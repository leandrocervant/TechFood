using AutoMapper;
using TechFood.Application.Categories.Models;
using TechFood.Domain.Entities;

namespace TechFood.Application.Common.AutoMapper
{
    public class DomainToDtoProfile : Profile
    {
        public DomainToDtoProfile()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
