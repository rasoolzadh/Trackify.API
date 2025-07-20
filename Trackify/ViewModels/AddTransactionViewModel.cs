// File: Trackify/ViewModels/AddTransactionViewModel.cs

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Trackify.Models;
using Trackify.Services;

namespace Trackify.ViewModels
{
    public partial class AddTransactionViewModel : BaseViewModel
    {
        private readonly TransactionService _transactionService;

        [ObservableProperty]
        decimal amount;

        [ObservableProperty]
        string note = string.Empty;

        [ObservableProperty]
        string selectedTransactionType = "Expense";

        [ObservableProperty]
        string selectedCategory = string.Empty;

        [ObservableProperty]
        DateTime date = DateTime.Now;

        public List<string> TransactionTypes { get; } = new List<string> { "Expense", "Income" };
        public List<string> Categories { get; } = new List<string> { "Food", "Transport", "Shopping", "Salary", "Bills", "Other" };

        public AddTransactionViewModel(TransactionService transactionService)
        {
            Title = "Add Transaction";
            _transactionService = transactionService;
        }

        [RelayCommand]
        async Task SaveTransactionAsync()
        {
            if (Amount <= 0 || string.IsNullOrWhiteSpace(Note) || string.IsNullOrWhiteSpace(SelectedCategory))
            {
                await Shell.Current.DisplayAlert("Validation Error", "Please fill all fields.", "OK");
                return;
            }

            var newTransaction = new Transaction
            {
                Amount = Amount,
                Note = Note,
                Date = Date,
                Type = SelectedTransactionType,
                Category = SelectedCategory
            };

            // ✅ CORRECT: Call the service only once and capture the result.
            var success = await _transactionService.AddTransactionAsync(newTransaction);

            if (success)
            {
                await Shell.Current.GoToAsync(".."); // Go back to the previous page
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Failed to save transaction.", "OK");
            }
        }
    }
}