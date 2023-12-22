using timetable_test_v1.ViewModels;

namespace timetable_test_v1.Views;

public partial class UserInformationPage : ContentPage
{
	public UserInformationPage(UserInfoVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}