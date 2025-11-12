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

            // UserControls (transient)       
            services.AddTransient<UcManualConfig>();
            services.AddTransient<UcOperations>();

            // Forms
            services.AddSingleton<AuthForm>();
            services.AddTransient<MainShellForm>();

            // Factory 
            services.AddSingleton<Func<MainShellForm>>(sp => () => sp.GetRequiredService<MainShellForm>());

            // Inventory (6×9)
            services.AddSingleton<IWarehouseInventory>(
                sp => new WarehouseInventory(rowCount: 6, columnCount: 9));


            var provider = services.BuildServiceProvider();

            ApplicationConfiguration.Initialize();
            WinFormsApp.Run(provider.GetRequiredService<AuthForm>());
        }
    }
}