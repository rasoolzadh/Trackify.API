// File: Trackify/Views/AddTransactionPage.xaml.cs
using Trackify.ViewModels;

namespace Trackify.Views // ✅ This must be correct
{
    public partial class AddTransactionPage : ContentPage
    {
        public AddTransactionPage(AddTransactionViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}