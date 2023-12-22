using timetable_test_v1.ViewModels;

namespace timetable_test_v1.Views;

public partial class SearchPage : ContentPage
{
	SearchPageVM vm;
	public SearchPage()
	{
		InitializeComponent();
		vm = new SearchPageVM();
		BindingContext = vm;
	}

	protected override async void OnAppearing()
	{
        base.OnAppearing();
        await vm.InitializeRealm();
    }


}