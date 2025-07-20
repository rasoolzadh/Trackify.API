using System.Diagnostics;
using System.Net.Http.Json;
using Trackify.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Trackify.Services
{
    public class TransactionService
    {
        private readonly HttpClient _httpClient;

        public TransactionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/transactions");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Transaction>>() ?? new List<Transaction>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching transactions: {ex.Message}");
                await Shell.Current.DisplayAlert("Connection Error", $"Could not connect to the server. Please check your connection and try again.\n\nDetails: {ex.Message}", "OK");
            }
            return new List<Transaction>();
        }

        public async Task<bool> AddTransactionAsync(Transaction transaction)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/transactions", transaction);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding transaction: {ex.Message}");
                await Shell.Current.DisplayAlert("Connection Error", $"Could not save the transaction. Please check your connection and try again.\n\nDetails: {ex.Message}", "OK");
                return false;
            }
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/transactions/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting transaction: {ex.Message}");
                await Shell.Current.DisplayAlert("Connection Error", $"Could not delete the transaction. Please check your connection and try again.\n\nDetails: {ex.Message}", "OK");
                return false;
            }
        }
    }
}