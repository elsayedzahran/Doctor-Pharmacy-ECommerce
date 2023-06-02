using ProjectTest.Models;

namespace ProjectTest.Services
{
    public interface ITokenService
    {
        public Task<string> CreateToken(UserEntity User);
    }
}
