using EmployeeMaintenance.Application.Commands.Users;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers.Users
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<User>>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.ToEntity();
            await _userRepository.AddAsync(user);
            return Result<User>.Persisted(user, string.Format(SuccessMessages.EntityCreatedWithSuccess, nameof(User)));
        }
    }
}