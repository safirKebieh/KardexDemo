using Application.Ports;
using Application.Warehouse;
using Application.Warehouse.Io;
using Infrastructure.Communication;
using Infrastructure.Time;
using Infrastructure.Warehouse;
using Microsoft.Extensions.DependencyInjection;
using WinFormsApp = System.Windows.Forms.Application;


namespace UI
{
    internal static class Program
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


            // Forms
            services.AddSingleton<AuthForm>();
            services.AddTransient<MainShellForm>();

            // Factory 
            services.AddSingleton<Func<MainShellForm>>(sp => () => sp.GetRequiredService<MainShellForm>());

            // Inventory (6×9)
            services.AddSingleton<IWarehouseInventory>(
                sp => new WarehouseInventory(rowCount: 6, columnCount: 9));

            // Address Encoder: Numerical 
            services.AddSingleton<ICraneAddressEncoder>(
                sp => new NumericalAddressEncoder(rowCount: 6, columnCount: 9));

            // UserControls (transient)
            services.AddTransient<UcStorageProcess>();         
            services.AddTransient<UcManualConfig>();
            services.AddTransient<UcRetrievePallet>();

            var provider = services.BuildServiceProvider();

            ApplicationConfiguration.Initialize();
            WinFormsApp.Run(provider.GetRequiredService<AuthForm>());
        }
    }
}