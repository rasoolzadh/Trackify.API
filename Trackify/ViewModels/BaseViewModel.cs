// File: Trackify/ViewModels/BaseViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;

namespace Trackify.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title = string.Empty; // Initialize here

        public bool IsNotBusy => !IsBusy;
    }
}