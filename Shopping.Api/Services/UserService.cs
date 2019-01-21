using Microsoft.Extensions.Options;
using Shopping.Api.Models;
using Shopping.Api.Options;
using Shopping.Api.Services.Interfaces;

namespace Shopping.Api.Services
{
    public class UserService : IUserService
    {
        private readonly UserSettings _settings;
        public UserService(IOptions<UserSettings> userSettings) => _settings = userSettings?.Value;

        public User GetUser()
        {
            return new User() {Name = _settings?.Name, Token = _settings?.Token};
        }

    }
}
