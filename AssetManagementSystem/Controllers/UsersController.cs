using AssetManagementSystem.Models;
using AssetManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        #region 1- Get All Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _repository.GetAllUsers();
            if (users == null)
            {
                return NotFound("No Users found");
            }
            return Ok(users.Value);
        }
        #endregion

        #region 3- Get User by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _repository.GetUserById(id);
            if (user == null)
            {
                return NotFound("No User found");
            }
            return Ok(user.Value);
        }
        #endregion

        #region 4- Insert a User - Return User Record
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                var newUser = await _repository.AddUser(user);
                if (newUser != null)
                {
                    return Ok(newUser.Value);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region 6- Update User
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            if (ModelState.IsValid)
            {
                var updatedUser = await _repository.UpdateUser(id, user);
                if (updatedUser != null)
                {
                    return Ok(updatedUser.Value);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region 7- Delete User
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var result = _repository.DeleteUser(id);
                if (result.Value == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "User could not be deleted or not found"
                    });
                }
                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                // Log exception in real-world scenarios
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexpected error occurred" });
            }
        }
        #endregion
    }
}
