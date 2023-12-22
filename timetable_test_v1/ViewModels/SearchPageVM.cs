using CommunityToolkit.Mvvm.ComponentModel;
using timetable_test_v1.Services;
using timetable_test_v1.Models;
using timetable_test_v1.Views;
using Realms;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MongoDB.Bson;

namespace timetable_test_v1.ViewModels
{
    public partial class SearchPageVM : BaseVM
    {
        private Realm realm;

        public SearchPageVM()
        {
            Buildings = new ObservableCollection<string>();
            PropertyChanged += OnViewModelPropertyChanged;
            Classrooms = new ObservableCollection<string>();
        }

        //create a initializeRealm
        public async Task InitializeRealm()
        {

            realm = SyncRealm.GetRealms();

            realm.Subscriptions.Update(() =>
            {
                var allClassroom = realm.All<Classroom>();
                realm.Subscriptions.Add(allClassroom);
            });

            await realm.Subscriptions.WaitForSynchronizationAsync();

            DoPicker();

            await Task.CompletedTask;
        }

        public void DoPicker()
        {
            _ = SaveBuilding();
        }

        [ObservableProperty]
        private ObservableCollection<string> classrooms;

        [ObservableProperty]
        private ObservableCollection<string> buildings;

        [ObservableProperty]
        private string? selectedClassroom;

        [ObservableProperty]
        private string? selectedBuilding;

        //create a method to save all building to the observable collection
        public async Task SaveBuilding()
        {
            IsBusy = true;
            try
            {
                //Buildings = new ObservableCollection<string>(realm.All<Classroom>()
                //    .ToList()
                //    .OrderBy(c => c.Building)
                //    .Select(c => c.Building)
                //    .Distinct()); 
                if ( Buildings.Count == 0)
                {
                    var getAllBuildingFunc = await App.RealmApp.CurrentUser.Functions.CallAsync<string[]>("get_all_building_name");
                
                    Buildings.Clear();
                
                    foreach (var item in getAllBuildingFunc)
                    {
                        Buildings.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            await Task.CompletedTask;

            IsBusy = false;
    
        }

        //create method to check if the selected building is changed
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedBuilding))
            {
                _ = SaveClassroom();
            }
        }

        // create a method to save all classroom name with the same building name to the observable collection
        public async Task SaveClassroom()
        {
            IsBusy = true;
            try
            {
                //Classrooms = new ObservableCollection<string>(realm.All<Classroom>()
                //.ToList()
                //.Where(c => c.Building == SelectedBuilding)
                //.OrderBy(c => c.Name)
                //.Select(c => c.Name));

                var getAllClassroomFunc = await App.RealmApp.CurrentUser.Functions.CallAsync<string[]>("get_all_classroom_name");

                Classrooms.Clear();

                var filteredClassrooms = getAllClassroomFunc.Where(item => item.Contains(SelectedBuilding)).ToArray();

                foreach (var item in filteredClassrooms)
                {
                    Classrooms.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");               
            }

            IsBusy = false;            
        }

        [RelayCommand]
        private async Task Navigate()
        {
            IsBusy = true;

            try
            {
                await Shell.Current.GoToAsync($"{nameof(ShowTimetablePage)}?ClassroomName={SelectedClassroom}");
            }
            catch(Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }

            IsBusy = false;
        }
    }
}
