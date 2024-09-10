using EmployeeCallendar.Controllers;
using EmployeeCallendar.Pages.LoginRegister;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using EmployeeCallendar.Pages.Home;
using EmployeeCallendar.ViewModel;

namespace EmployeeCallendar
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            ConfigureServices(builder.Services);

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EmployeesCallendarDbContext>();
            services.AddSingleton<IAuthenticationController, AuthenticationController>();

            services.AddTransient<Login>();
            services.AddTransient<AddEmployee>();
            services.AddSingleton<AddEmployeeViewModel>();
            services.AddTransient<Pages.Home.EmployeeCallendar>();
            services.AddSingleton<EmployeeCallendarViewModel>();
        }
    }
}
