using System.Diagnostics;
using timetable_test_v1.Models;
using timetable_test_v1.ViewModels;

namespace timetable_test_v1.Views;
[QueryProperty(nameof(ClassroomName), nameof(ClassroomName))]
public partial class ShowTimetablePage : ContentPage
{
	ShowTimetablePageVM vm;

	public string ClassroomName
    {
        set => vm.Classroom_Name = value;
    }

    public ShowTimetablePage()
	{
		InitializeComponent();
		vm = new ShowTimetablePageVM(Navigation);
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
        vm.IsPageActive = false;

		if(!vm.IsPageActive)
		{
			vm.NavigationBack();
		}
    }
}
