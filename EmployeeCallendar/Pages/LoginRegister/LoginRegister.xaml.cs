namespace EmployeeCallendar.Pages.LoginRegister;

public partial class LoginRegister : ContentPage
{
    public IList<IView> BasePageContent => BaseContent.Children;
    public LoginRegister()
	{
		InitializeComponent();
	}
}