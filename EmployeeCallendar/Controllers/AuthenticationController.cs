using BCrypt.Net;
using EmployeeCallendar.Controllers;
using EmployeeCallendar.Models;

namespace EmployeeCallendar.Controllers;

public interface IAuthenticationController
{
    bool AuthenticateUser(string username, string password);
    //Task<bool> AutoLoginAuthentication();
    Task<bool> LogOut();
    void RegisterUser();
}

public class AuthenticationController : IAuthenticationController
{
    private readonly EmployeesCallendarDbContext _dbContext;
    private UserModel user;

    public AuthenticationController(EmployeesCallendarDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool AuthenticateUser(string username, string password)
    {
        UserModel? user = _dbContext.User.SingleOrDefault(u => u.username == username);
        if (user == null)
        {
            return false;
        }

        this.user = user;
        return BCrypt.Net.BCrypt.Verify(password, user.password);
    }

    /*public async Task<bool> AutoLoginAuthentication()
    {
        string usrname = await SecureStorage.GetAsync("username");
        string pass = await SecureStorage.GetAsync("password");

        if (usrname != null && pass != null)
        {
            bool isAuthenticated = AuthenticateUser(usrname, pass);

            if (isAuthenticated)
            {
                User user = GetUserByUsername(usrname);
                this.user = new UserController(user);
            }

            return isAuthenticated;
        }

        return false;
    }*/

    public void RegisterUser()
    {
        _dbContext.User.Add(new UserModel
        {
            email = "adam@ex2aple.com",
            FirstName = "Adam",
            LastName = "Cacak",
            phone = "123456789",
            role = "administrator",
            username = "adam",
            password = BCrypt.Net.BCrypt.HashPassword("adam123")
        });
        _dbContext.SaveChanges();
    }

    public async Task<bool> LogOut()
    {
        SecureStorage.Remove("username");
        SecureStorage.Remove("password");

        this.user = null;

        return true;
    }
}