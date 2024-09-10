namespace EmployeeCallendar.Pages.LoginRegister;

using EmployeeCallendar.Controllers;
using EmployeeCallendar.Models;

public partial class Login : LoginRegister
{
    public IAuthenticationController AuthController;

	public Login(IAuthenticationController auth)
	{
		InitializeComponent();
        AuthController = auth;
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        if (AuthController != null)
        {
            if (AuthController.AuthenticateUser(username.Text, password.Text))
            {
                App.Current.MainPage = new AppShell();
            }
        }
    }
}