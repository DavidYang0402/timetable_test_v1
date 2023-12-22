using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace timetable_test_v1.ViewModels
{
    public partial class BaseVM : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(isNotBusy))]

        bool isBusy;
        public bool isNotBusy => !IsBusy;
    }   
}
