using EmployeeCallendar.Controllers;
using EmployeeCallendar.Pages.Home;
using EmployeeCallendar.Pages.LoginRegister;
using EmployeeCallendar.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace EmployeeCallendar
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            InitializeComponent();
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            //MainPage = Handler.MauiContext.Services.GetRequiredService<Login>();
            MainPage = new AppShell();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EmployeesCallendarDbContext>(options =>
                options.UseSqlServer(
                    "Data Source = localhost\\SQLEXPRESS; Initial Catalog = EmployeesCallendar; Integrated Security = True; Trust Server Certificate = True"));

            services.AddSingleton<IAuthenticationController, AuthenticationController>();

            services.AddTransient<Login>();
        }
    }
}
