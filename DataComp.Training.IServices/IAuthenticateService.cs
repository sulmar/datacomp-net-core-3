using DataComp.Training.Models;

namespace DataComp.Training.IServices
{
    public interface IAuthenticateService
    {
        bool TryAuthenticate(string username, string hashedPassword, out User user);
    }
}
