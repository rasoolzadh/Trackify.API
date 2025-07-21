
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Trackify.Models;
using Trackify.Services;
using Trackify.Views;

namespace Trackify.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly TransactionService _transactionService;

        public ObservableCollection<Transaction> Transactions { get; } = new();

        [ObservableProperty]
        decimal balance;

        [ObservableProperty]
        decimal income;

        [ObservableProperty]
        decimal expense;

        public MainViewModel(TransactionService transactionService)
        {
            Title = "Trackify";
            _transactionService = transactionService;
        }

        [RelayCommand]
        async Task GetTransactionsAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var transactions = await _transactionService.GetTransactionsAsync();

                if (Transactions.Count != 0)
                    Transactions.Clear();

                foreach (var transaction in transactions)
                    Transactions.Add(transaction);

                CalculateSummary();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get transactions: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        void CalculateSummary()
        {
            Income = Transactions.Where(t => t.Type == "Income").Sum(t => t.Amount);
            Expense = Transactions.Where(t => t.Type == "Expense").Sum(t => t.Amount);
            Balance = Income - Expense;
        }

        [RelayCommand]
        async Task GoToAddTransactionPage()
        {
            await Shell.Current.GoToAsync(nameof(AddTransactionPage));
        }

        [RelayCommand]
        async Task DeleteTransactionAsync(int id)
        {
            if (await _transactionService.DeleteTransactionAsync(id))
            {
                var transactionToRemove = Transactions.FirstOrDefault(t => t.Id == id);
                if (transactionToRemove != null)
                {
                    Transactions.Remove(transactionToRemove);
                    CalculateSummary();
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Failed to delete transaction.", "OK");
            }
        }
    }
}