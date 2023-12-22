using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Bson;
using Newtonsoft.Json;
using Realms;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using timetable_test_v1.Models;
using timetable_test_v1.Services;

namespace timetable_test_v1.ViewModels
{
    public partial class ShowTimetablePageVM : BaseVM
    {
        [ObservableProperty]
        private bool isPageActive;

        [ObservableProperty]
        public string classroom_Name;

        private Realm realm;

        [ObservableProperty]
        private ObservableCollection<Timetable> timetableList;

        [ObservableProperty]
        private ObservableCollection<string> day_of_week;

        [ObservableProperty]
        private string selectedDate;
        partial void OnSelectedDateChanged(string value)
        {
            LoadTimetable();
        }

        [ObservableProperty]
        private string course;

        [ObservableProperty]
        private string instructor;

        [ObservableProperty]
        private string classname;

        [ObservableProperty]
        private string status;

        [ObservableProperty]
        private Timetable selectedTimetable;

        //檢查是否還在這個頁面
        [ObservableProperty]
        private INavigation navigation;
        public ShowTimetablePageVM(INavigation navigation)
        {
            this.navigation = navigation;
            //Task get = InitializeRealm();
            Day_of_week = new ObservableCollection<string>();
            TimetableList = new ObservableCollection<Timetable>();
            SelectedTimetable = TimetableList.Skip(0).FirstOrDefault();
        }

        public void NavigationBack()
        {
            Navigation.PopAsync();
        }

        public async Task InitializeRealm()
        {
            realm = SyncRealm.GetRealms();

            realm.Subscriptions.Update(() =>
            {
                var allClassroom = realm.All<Classroom>();
                var allTimetable = realm.All<Timetable>();
                realm.Subscriptions.Add(allClassroom);
                realm.Subscriptions.Add(allTimetable);
            });

            await realm.Subscriptions.WaitForSynchronizationAsync();

            await GetDayOfWeek();
                
            await Task.CompletedTask;
        }

        private async Task GetDayOfWeek()
        {
            IsBusy = true;

            try
            {
                if(Day_of_week.Count == 0)
                {

                    var getDayOfWeek  = await App.RealmApp.CurrentUser.Functions.CallAsync<IList<string>>("get_day_of_week");
                    
                    Day_of_week.Clear();

                    foreach(var item in getDayOfWeek)
                    {
                        Day_of_week.Add(item);
                    }                   
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
            }

            await Task.CompletedTask;

            IsBusy = false;

        }

        //create a method to get one classroom timetable and collection view it
        private async void LoadTimetable()
        {
            IsBusy = true;

            try
            {
                var getClassroomTimetable = await Task.Run(async () =>
                {
                    return await App.RealmApp.CurrentUser.Functions
                        .CallAsync<BsonDocument>
                        ("get_timetable_of_Date", Classroom_Name, SelectedDate);
                });
                TimetableList.Clear();

                foreach(var item in getClassroomTimetable["data"].AsBsonArray)
                {
                    var timetable = JsonConvert.DeserializeObject<Timetable>(item.ToString());

                    TimetableList.Add(timetable);

                    //Debug.WriteLine(item + "\n");
                }
                //Debug.WriteLine($"Time table listCount : {TimetableList.Count}");
                //Debug.WriteLine($"Is Choose Date : {SelectedDate}");
            }
            catch(Exception ex) 
            {
                await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
            }

            IsBusy = false;
        }

        [RelayCommand]
        private void Course_On_Tapped(SelectionChangedEventArgs args)
        {
            Debug.WriteLine("Course On Tapped");

        }
    }
}
