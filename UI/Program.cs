using Application.Ports;
using Application.UseCases;
using Application.UseCases.Handlers;
using Application.Warehouse;
using Application.Warehouse.Io;
using Infrastructure.Communication;
using Infrastructure.Time;
using Infrastructure.Warehouse;

using Microsoft.Extensions.DependencyInjection;
using WinFormsApp = System.Windows.Forms.Application;


namespace UI
{
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();

            // DI registrations
            services.AddSingleton<IModbusService, ModbusService>();
            services.AddSingleton<IClock, SystemClock>();
            services.AddSingleton<IWarehouseIo, ModbusWarehouseIo>();

            services.AddSingleton<IStorePalletUseCase, StorePalletUseCase>();
            services.AddSingleton<IRetrievePalletUseCase, RetrievePalletUseCase>();

            services.AddSingleton<IResetOutputsUseCase, ResetOutputsUseCase>();

            // UserControls      
            services.AddTransient<UcDashboard>();
            services.AddTransient<UcWarehouseOp>();
            services.AddTransient<UcManualControl>();

            // Forms
            services.AddSingleton<AuthForm>();
            services.AddTransient<MainShellForm>();

            // Inventory
            services.AddSingleton<IWarehouseInventory>(
                sp => new WarehouseInventory(rowCount: 6, columnCount: 9));

            var provider = services.BuildServiceProvider();

            ApplicationConfiguration.Initialize();
            WinFormsApp.Run(provider.GetRequiredService<AuthForm>());
        }
    }
}