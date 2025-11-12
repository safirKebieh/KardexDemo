using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace UI
{
    public partial class UcOperations : UserControl
    {
        private readonly IStorePalletUseCase _store;
        private readonly IRetrievePalletUseCase _retrieve;
        private readonly IResetOutputsUseCase _reset;
        private CancellationTokenSource? _cts;

        public UcOperations(IStorePalletUseCase store, IRetrievePalletUseCase retrieve, IResetOutputsUseCase reset)
        {
            InitializeComponent();
            _store = store;
            _retrieve = retrieve;

            cmbMode.SelectedIndex = 0;
            txtLog.ReadOnly = true; 
            _reset = reset;
        }

        private void AppendLog(string msg)
        {
            if (txtLog.IsDisposed) return;
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action<string>(AppendLog), msg);
                return;
            }
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            AppendLog("Start clicked"); // للتأكد أن الحدث موصول

            btnStart.Enabled = false;

            try
            {
                _cts?.Dispose();
                _cts = new CancellationTokenSource();

                var slotNumber = (int)numSlot.Value;
                var progress = new Progress<string>(AppendLog);

                bool ok;
                var mode = cmbMode.SelectedItem?.ToString();

                if (string.Equals(mode, "Retrieve", StringComparison.OrdinalIgnoreCase))
                {
                    AppendLog($"Retrieve started. Slot={slotNumber}");
                    ok = await _retrieve.RunAsync(slotNumber, progress, _cts.Token);
                }
                else
                {
                    AppendLog($"Store started. Slot={slotNumber}");
                    ok = await _store.RunAsync(slotNumber, progress, _cts.Token);
                }

                AppendLog(ok ? "Completed successfully." : "Completed with errors.");
            }
            catch (OperationCanceledException)
            {
                AppendLog("Operation canceled.");
            }
            catch (Exception ex)
            {
                AppendLog("Error: " + ex.Message);
            }
            finally
            {
                btnStart.Enabled = true;
            }
        }

        private async void btnQuitterung_Click(object sender, EventArgs e)
        {
            AppendLog("Quitterung clicked");

            btnQuitterung.Enabled = false;

            try
            {
                // Ask the new use case to execute the reset logic
                var progress = new Progress<string>(AppendLog);
                await _reset.RunAsync(progress, _cts?.Token ?? CancellationToken.None);
                AppendLog("✅ All outputs reset successfully.");
            }
            catch (Exception ex)
            {
                AppendLog($"❌ Quitterung failed: {ex.Message}");
            }
            finally
            {
                btnQuitterung.Enabled = true;
            }
        }
    }
}
