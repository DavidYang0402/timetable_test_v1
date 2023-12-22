using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace timetable_test_v1.ViewModels
{
    public partial class UserInfoVM : BaseVM
    {
        [RelayCommand]
        public async Task Logout()
        {
            try
            {
                // logout 未處理
                //await App.RealmApp.CurrentUser.LogOutAsync();
                await Shell.Current.GoToAsync("///Login");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error Logout", ex.Message, "OK");
            }
            await Task.CompletedTask;
        }

        //create 
    }

}
