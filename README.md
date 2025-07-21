# Trackify - Cross-Platform Expense Tracker

Trackify is a cross-platform expense and income tracking application built with .NET MAUI and an ASP.NET Core Web API backend. It allows users to manage their finances, categorize transactions, and visualize their spending habits through charts.

---

##  Features

- **Add Transactions**: Log income and expense records with amount, category, and notes.
- **Categorization**: Assign transactions to predefined categories like Food, Salary, Shopping, etc.
- **Dashboard**: View a real-time summary of your total balance, income, and expenses.
- **History**: See a list of all your past transactions.
- **Data Visualization**: An interactive donut chart breaks down expenses by category.
- **Persistent Storage**: All data is stored in a SQL Server database via a backend API.

---

##  Tech Stack

- **Frontend**: .NET MAUI, XAML, MVVM
- **Backend**: ASP.NET Core Web API
- **Database**: SQL Server & Entity Framework Core
- **Charting**: Microcharts
- **IDE**: Visual Studio 2022/2023

---

##  Prerequisites

- .NET 8 SDK
- Visual Studio 2022 with the **.NET Multi-platform App UI development** workload installed.
- SQL Server (any edition: Express, Developer, etc.).

---

##  Setup & Installation

To get this project running locally, follow these steps:

#### 1. Configure the Backend (Trackify.API)

1.  Open the `Trackify.API/appsettings.json` file.
2.  Update the `DefaultConnection` string with your SQL Server credentials.
3.  Open the **Package Manager Console** in Visual Studio (`View` -> `Other Windows` -> `Package Manager Console`).
4.  Set the default project to `Trackify.API`.
5.  Run the following commands to create the database:
    ```powershell
    Add-Migration InitialCreate
    Update-Database
    ```

#### 2. Configure the Frontend (Trackify)

1.  Open the `Trackify/MauiProgram.cs` file.
2.  Find the `baseApiUrl` variable.
3.  **For Physical Device Testing**: You must replace the placeholder IP address with your PC's local IP address.
    - Find your IP by opening Command Prompt (`cmd`) and typing `ipconfig`.
    - Ensure your phone and PC are on the **same Wi-Fi network**.
    ```csharp
    // Example using your PC's IP address
    string baseApiUrl = DeviceInfo.Platform == DevicePlatform.Android
                            ? "http://YOUR_PC_IP_ADDRESS:5138" 
                            : "http://localhost:5138";
    ```
---

##  Running the Application

1.  In Visual Studio's Solution Explorer, right-click the **Solution** -> **Properties**.
2.  Select **Multiple startup projects**.
3.  Set the **Action** for both `Trackify.API` and `Trackify` to **Start**.
4.  Click **OK**.
5.  Select your target (Windows Machine or your Android device) and click the **Run** button.

---

##  Troubleshooting

If the Android app fails to connect (timeout or connection refused):

- **Check Firewall**: Ensure your Windows Firewall has an **Inbound Rule** to allow connections on your API's HTTP port (e.g., TCP Port `5138`).
- **Check API Binding**: Ensure your `Trackify.API/Properties/launchSettings.json` file uses `http://0.0.0.0:5138` or `http://*:5138` to accept connections from the network.
- **Check Network**: Confirm your phone and PC are on the exact same Wi-Fi network and that "AP Isolation" (or "Client Isolation") is disabled on your router.
