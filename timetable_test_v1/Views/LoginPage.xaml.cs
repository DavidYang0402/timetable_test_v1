using timetable_test_v1.ViewModels;

namespace timetable_test_v1.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageVM vm)
	{
		InitializeComponent();
		BindingContext = vm;

	}
}