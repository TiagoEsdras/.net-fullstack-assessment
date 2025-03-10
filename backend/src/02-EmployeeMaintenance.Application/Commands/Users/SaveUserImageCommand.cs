using EmployeeMaintenance.Application.Shared;
using MediatR;

namespace EmployeeMaintenance.Application.Commands.Users
{
    public class SaveUserImageCommand : IRequest<Result<string>>
    {
        public SaveUserImageCommand(string photoBase64, string userName)
        {
            PhotoBase64 = photoBase64;
            UserName = userName;
        }

        public string PhotoBase64 { get; set; }
        public string UserName { get; set; }
    }
}