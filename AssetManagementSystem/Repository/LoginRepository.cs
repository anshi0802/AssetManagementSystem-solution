using AssetManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AssetManagementSystem.Repository
{
   


            public class LoginRepository : ILoginRepository
{
    private readonly AssetManagementDbContext _context;

    public LoginRepository(AssetManagementDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // ValidateUser method here



        #region Validate User
        public async Task<Login?> ValidateUser(string username, string password)
{
    try
    {
        if (_context == null)
        {
            throw new InvalidOperationException("Database context is not initialized");
        }

        Login? dbUser = await _context.Logins.FirstOrDefaultAsync(
            u => u.Username == username && u.Password == password);

        return dbUser;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error in ValidateUser: {ex.Message}");
        return null;
    }
}

        //public async Task<Login?> ValidateUser(string username, string password)
        //{
        //    try
        //    {
        //        if (_context != null)
        //        {
        //            Login? dbUser = await _context.Logins.FirstOrDefaultAsync(
        //                u => u.Username == username && u.Password == password);
        //            return dbUser;
        //        }
        //        // Return null if _context is null
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it as needed
        //        return null;
        //    }
        //}
        #endregion
    }
}

//https://localhost:7052/api/Logins/adminUser/adminPass123