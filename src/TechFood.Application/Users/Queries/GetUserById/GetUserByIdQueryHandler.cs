using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TechFood.Application.Users.Data;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;

namespace TechFood.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler(
        IMapper mapper,
        IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            return _mapper.Map<User, UserDto>(user);
        }
    }
}
