// File: Trackify/ViewModels/ChartsViewModel.cs
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microcharts;
using SkiaSharp;
using Trackify.Models;
using Trackify.Services;

namespace Trackify.ViewModels
{
    public partial class ChartsViewModel : BaseViewModel
    {
        private readonly TransactionService _transactionService;

        [ObservableProperty]
        Chart expenseChart;

        public ChartsViewModel(TransactionService transactionService)
        {
            Title = "Charts";
            _transactionService = transactionService;
        }

        [RelayCommand]
        public async Task LoadChartDataAsync()
        {
            var transactions = await _transactionService.GetTransactionsAsync();
            var expenseTransactions = transactions.Where(t => t.Type == "Expense");

            var categoryGroups = expenseTransactions
                .GroupBy(t => t.Category)
                .Select(group => new ChartEntry((float)group.Sum(t => t.Amount))
                {
                    Label = group.Key,
                    ValueLabel = $"{group.Sum(t => t.Amount):C}",
                    Color = SKColor.Parse(GetRandomColor())
                })
                .ToList();

            ExpenseChart = new DonutChart
            {
                Entries = categoryGroups,
                LabelTextSize = 30f,
                BackgroundColor = SKColors.Transparent
            };
        }

        private string GetRandomColor()
        {
            var random = new Random();
            return $"#{random.Next(0x1000000):X6}";
        }
    }
}