using timetable_test_v1.Views;

namespace timetable_test_v1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage),typeof(LoginPage));
            Routing.RegisterRoute(nameof(HomePage),typeof(HomePage));
            Routing.RegisterRoute(nameof(SearchPage),typeof(SearchPage));
            Routing.RegisterRoute(nameof(UserInformationPage), typeof(UserInformationPage));
            Routing.RegisterRoute(nameof(ShowTimetablePage), typeof(ShowTimetablePage));

        }
    }
}
