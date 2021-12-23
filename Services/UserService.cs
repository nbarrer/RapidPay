using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RapidPayAPI.Data;
using RapidPayAPI.Entities;
using RapidPayAPI.Interfaces;

namespace RappidPayAPI.Services
{

    public class UserService : IUserService
    {
        private readonly RapidPayContext _dbcontext;
        public UserService(RapidPayContext context)
        {
            _dbcontext = context;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            // wrapped in "await Task.Run" to mimic fetching user from a db
            var user = await Task.Run(() => _dbcontext.User.SingleOrDefault(x => x.Username == username && x.Password == password));
            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            // wrapped in "await Task.Run" to mimic fetching users from a db
            return await Task.Run(() => _dbcontext.User);
        }
    }
}