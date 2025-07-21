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
                var response = await _httpClient.GetAsync("api/transactions");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<Transaction>>() ?? new List<Transaction>();
            }
            catch (HttpRequestException httpEx)
            {
                Debug.WriteLine($"HTTP Error: {httpEx.Message}");
                await ShowAlert("Connection Error",
                    $"Server returned an error: {httpEx.StatusCode?.ToString() ?? "Unknown"}");
            }
            catch (TaskCanceledException)
            {
                Debug.WriteLine("Request timed out");
                await ShowAlert("Timeout Error",
                    "The request took too long. Please check your connection and try again.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"General Error: {ex.Message}");
                await ShowAlert("Error",
                    $"An unexpected error occurred: {ex.Message}");
            }
            return new List<Transaction>();
        }

        public async Task<bool> AddTransactionAsync(Transaction transaction)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/transactions", transaction);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding transaction: {ex.Message}");
                await ShowAlert("Error",
                    $"Failed to add transaction: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/transactions/{id}");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting transaction: {ex.Message}");
                await ShowAlert("Error",
                    $"Failed to delete transaction: {ex.Message}");
                return false;
            }
        }

        private async Task ShowAlert(string title, string message)
        {
            try
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert(title, message, "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error showing alert: {ex.Message}");
            }
        }
    }
}