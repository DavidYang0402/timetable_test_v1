using Realms;
using timetable_test_v1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using timetable_test_v1.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Windows.Input;

namespace timetable_test_v1.ViewModels
{
    public partial class HomePageVM : BaseVM
    {

        private Realm realm;

        //輪播宣告
        [ObservableProperty]
        private ObservableCollection<ImageButton> kioskItems;

        //數據宣告
        [ObservableProperty]
        int classroomQuantity;

        [ObservableProperty]
        int buildingQuantity;

        [ObservableProperty]
        private ImageButton currentItem;
        private int _currentIndex = 0;
        private Timer _timer;

        public HomePageVM()
        {
            //Task connectRealm = InitializeRealm();
            KioskItems = new ObservableCollection<ImageButton>();
            StartAutoPlay();
            ClassroomQuantity = 0;
            BuildingQuantity = 0;
        }

        public async Task InitializeRealm()
        {
            realm = SyncRealm.GetRealms();

            realm.Subscriptions.Update(() =>
            {
                var allClassroom = realm.All<Classroom>();
                realm.Subscriptions.Add(allClassroom);

            });

            await realm.Subscriptions.WaitForSynchronizationAsync();

            await UpdateData();

            await Task.CompletedTask;
        }

        //create a method to call GetClassroomQuantity
        public async Task UpdateData()
        {
            IsBusy = true;

            await GetKioskItem();

            await GetClassroomQuantity();

            await GetBuildingQuantity();

            await Task.CompletedTask;

            IsBusy = false;
        }

        [ObservableProperty]
        private string source;

        [ObservableProperty]
        private ICommand command;

        [ObservableProperty]
        private Uri imageURI;

        //create a method to get all the classrooms
        public async Task GetKioskItem()
        {
            IsBusy = true;
            try
            {
                KioskItems = new ObservableCollection<ImageButton>()
                {
                    new ImageButton
                    {
                        Source = "img1.jpg",
                        Command = new Command(() =>  ToEvenLink("https://www.google.com"))
                    },
                    new ImageButton
                    {
                        Source = "img2.jpg",
                        Command = new Command(() =>  ToEvenLink("sfvskof2012.github.io"))
                    },
                    new ImageButton
                    {
                        Source = "img3.jpg",
                        Command = new Command(() =>  ToEvenLink("sfvskof2012.github.io"))
                    },
                    new ImageButton
                    {
                        Source = "img4.jpg",
                        Command = new Command(() => ToEvenLink("sfvskof2012.github.io"))
                    }
                };
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            await Task.CompletedTask;

            IsBusy = false;
        }

        public async void ToEvenLink(string evenLink)
        {
            try
            {
                if (Uri.TryCreate(evenLink, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                {
                    await Browser.OpenAsync(uriResult, BrowserLaunchMode.SystemPreferred);
                }
                else
                {
                    Uri uri = new($"https://{evenLink}");
                    await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
                }              
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        //create a method to get all the classrooms quantity
        public async Task GetClassroomQuantity()
        {
            IsBusy = true;

            try
            {
                var ClassroomCountResult = await App.RealmApp.CurrentUser.Functions.CallAsync<Int64>("Count_Classroom");
                ClassroomQuantity = (int)ClassroomCountResult;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            await Task.CompletedTask;

            IsBusy = false;
        }

        public async Task GetBuildingQuantity()
        {
            IsBusy = true;

            try
            {
                var buildingCountResult = await App.RealmApp.CurrentUser.Functions.CallAsync<Int64>("Count_Building");
                BuildingQuantity = (int)buildingCountResult;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            await Task.CompletedTask;

            IsBusy = false;
        }


        private void StartAutoPlay()
        {
            _timer = new Timer(TimerCallback, null, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(3));
        }

        private void TimerCallback(object state)
        {
            _currentIndex = (_currentIndex + 1) % KioskItems.Count;
            OnCurrentItemChanged(KioskItems[_currentIndex]);
        }

        partial void OnCurrentItemChanged(ImageButton value)
        {
            CurrentItem = value;
        }
    }
}