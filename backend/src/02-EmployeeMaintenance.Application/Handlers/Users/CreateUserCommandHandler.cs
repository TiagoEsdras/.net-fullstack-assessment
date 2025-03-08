using AutoMapper;
using EmployeeMaintenance.Application.Commands.Users;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers.Users
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserResponseDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.ToEntity();
            await _userRepository.AddAsync(user);
            var userDto = _mapper.Map<UserResponseDto>(user);
            return Result<UserResponseDto>.Persisted(userDto, string.Format(SuccessMessages.EntityCreatedWithSuccess, nameof(User)));
        }
    }
}