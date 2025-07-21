
using Trackify.ViewModels;

namespace Trackify.Views;

public partial class ChartsPage : ContentPage
{
    public ChartsPage(ChartsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}