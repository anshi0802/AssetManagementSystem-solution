using AssetManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagementSystem.Repository
{
    public interface IUserRepository
    {
        #region 1- Get All Users - Search All
        // Get all users from DB
        // Search All
        public Task<ActionResult<IEnumerable<User>>> GetAllUsers();
        #endregion

        // 3 - Get a User based on Id
        public Task<ActionResult<User>> GetUserById(int id);

        // 4 - Insert a User - Return User Record
        public Task<ActionResult<User>> AddUser(User user);

        // 6 - Update a User with ID and User
        public Task<ActionResult<User>> UpdateUser(int id, User user);

        // 7 - Delete a User
        public JsonResult DeleteUser(int id);
    }
}
