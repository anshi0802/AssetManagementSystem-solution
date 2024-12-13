using AssetManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagementSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AssetManagementDbContext _context;

        public UserRepository(AssetManagementDbContext context)
        {
            _context = context;
        }

        #region 1- Get All Users - Search All
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Users.ToListAsync();
                }
                return new ActionResult<IEnumerable<User>>(new List<User>());
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return new ActionResult<IEnumerable<User>>((IEnumerable<User>)null);
            }
        }
        #endregion

        #region 3- Get a User based on Id
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                if (_context != null)
                {
                    var user = await _context.Users.FindAsync(id);
                    return user != null ? new ActionResult<User>(user) : new ActionResult<User>((User)null);
                }
                return new ActionResult<User>((User)null);
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return new ActionResult<User>((User)null);
            }
        }
        #endregion

        #region 4- Insert a User - Return User Record
        public async Task<ActionResult<User>> AddUser(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "User data is null");
                }

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                var addedUser = await _context.Users.FindAsync(user.UId);
                return addedUser != null ? new ActionResult<User>(addedUser) : new ActionResult<User>((User)null);
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return new ActionResult<User>((User)null);
            }
        }
        #endregion

        #region 6- Update a User with ID and User
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "User data is null");
                }

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                var existingUser = await _context.Users.FindAsync(id);
                if (existingUser == null)
                {
                    return new ActionResult<User>((User)null);
                }

                // Map values from the input user to the existing user
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Age = user.Age;
                existingUser.Gender = user.Gender;
                existingUser.Address = user.Address;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.LId = user.LId;

                await _context.SaveChangesAsync();

                return new ActionResult<User>(existingUser);
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return new ActionResult<User>((User)null);
            }
        }
        #endregion

        #region 7- Delete a User
        public JsonResult DeleteUser(int id)
        {
            try
            {
                if (_context == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Database context is not initialized."
                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }

                var existingUser = _context.Users.Find(id);
                if (existingUser == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "User not found."
                    })
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                _context.Users.Remove(existingUser);
                _context.SaveChangesAsync();

                return new JsonResult(new
                {
                    success = true,
                    message = "User deleted successfully."
                })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return new JsonResult(new
                {
                    success = false,
                    message = "An error occurred while deleting the user."
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        #endregion
    }
}
