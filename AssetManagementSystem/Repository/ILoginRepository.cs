using AssetManagementSystem.Models;
using System.Threading.Tasks;

namespace AssetManagementSystem.Repository
{
    public interface ILoginRepository
    {
        // Get user details by username and password
        public Task<Login?> ValidateUser(string username, string password);
    }
}
