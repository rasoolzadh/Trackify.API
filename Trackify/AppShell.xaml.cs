// File: Trackify/AppShell.xaml.cs
using Trackify.Views;

namespace Trackify;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(AddTransactionPage), typeof(AddTransactionPage));
    }
}