using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Realms.Sync;


namespace timetable_test_v1.ViewModels
{
    public partial class LoginPageVM : BaseVM
    {
        

        public LoginPageVM()
        {
            account = "admin@admin.com";
            password = "admin1234";
        }

        [ObservableProperty]
        string account;

        [ObservableProperty]
        string password;

        [RelayCommand]
        public async Task Login()
        {       
            IsBusy = true;
            try
            {
                var credentials = Credentials.EmailPassword(Account, Password);
                var user = await App.RealmApp.LogInAsync(credentials);
                if (user != null)
                {
                    await Task.Delay(500);
                    await Shell.Current.GoToAsync("///Home");
                    Account = "";
                    Password = "";                   
                }
                else
                {
                    throw new Exception("Login Failed");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error Login", ex.Message, "OK");
            }

            await Task.CompletedTask;

            IsBusy = false;

        }

        //create method for register
        [RelayCommand]
        public async Task Register()
        {
            try
            {
                await App.RealmApp.EmailPasswordAuth.RegisterUserAsync(Account, Password);
                await Login();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error Register", ex.Message, "OK");
            }

            await Task.CompletedTask;
        }

        [RelayCommand]
        public async Task Anon_Login()
        {
            IsBusy = true;

            try
            {
                await App.RealmApp.LogInAsync(Credentials.Anonymous());

                await Task.Delay(500);
                await Shell.Current.GoToAsync("///Home");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error Login", ex.Message, "OK");
            }

            await Task.CompletedTask;

            IsBusy = false;
        }
    }
}