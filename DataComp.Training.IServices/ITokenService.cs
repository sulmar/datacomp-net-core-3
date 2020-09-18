using DataComp.Training.Models;

namespace DataComp.Training.IServices
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
