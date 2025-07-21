
using Trackify.ViewModels;

namespace Trackify.Views 
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