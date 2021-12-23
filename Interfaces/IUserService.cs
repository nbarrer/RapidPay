using RapidPayAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RapidPayAPI.Interfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }
}
