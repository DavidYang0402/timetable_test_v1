using Microsoft.Extensions.Logging;
using timetable_test_v1.ViewModels;
using timetable_test_v1.Views;

namespace timetable_test_v1
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<LoginPageVM>();

            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<HomePageVM>();

            builder.Services.AddSingleton<SearchPage>();
            builder.Services.AddSingleton<SearchPageVM>();

            builder.Services.AddTransient<ShowTimetablePage>();
            builder.Services.AddTransient<ShowTimetablePageVM>();

            builder.Services.AddSingleton<UserInformationPage>();
            builder.Services.AddSingleton<UserInfoVM>();

            return builder.Build();
        }
    }
}
