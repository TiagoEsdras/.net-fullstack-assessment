using Bogus;
using EmployeeMaintenance.Application.Commands.Users;

namespace EmployeeMaintenance.Tests.Builders.Commands.Users
{
    public class SaveUserImageCommandBuilder
    {
        private readonly Faker _faker = new();
        private readonly SaveUserImageCommand _instance;

        public SaveUserImageCommandBuilder()
        {
            _instance = new SaveUserImageCommand
            {
                UserName = $"{_faker.Name.FirstName() + _faker.Name.LastName()}",
                PhotoBase64 = Convert.ToBase64String(new byte[] { 0xFF, 0xD8, 0xFF, 0x4E })
            };
        }

        public SaveUserImageCommandBuilder WithUserName(string username)
        {
            _instance.UserName = username;
            return this;
        }

        public SaveUserImageCommandBuilder WithPhotoBase64(string photoBase64)
        {
            _instance.PhotoBase64 = photoBase64;
            return this;
        }

        public SaveUserImageCommand Build() => _instance;
    }
}