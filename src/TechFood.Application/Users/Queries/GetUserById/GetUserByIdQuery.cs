using MediatR;
using TechFood.Application.Users.Data;

namespace TechFood.Application.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(int Id) : IRequest<UserDto>;
}
