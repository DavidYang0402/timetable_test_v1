using timetable_test_v1.ViewModels;

namespace timetable_test_v1.Views;

public partial class HomePage : ContentPage
{
	HomePageVM vm;
	public HomePage()
	{
		InitializeComponent();
		vm = new HomePageVM();
		BindingContext = vm;
	}

	protected override async void OnAppearing()
	{
        base.OnAppearing();
		await vm.InitializeRealm();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }
}