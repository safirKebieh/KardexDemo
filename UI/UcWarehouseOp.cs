using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace UI
{
    public partial class UcWarehouseOp : UserControl
    {
        private readonly IStorePalletUseCase _store;
        private readonly IRetrievePalletUseCase _retrieve;
        private readonly IResetOutputsUseCase _reset;
        private readonly IClearAllSlotsUseCase _clearSlots;

        private CancellationTokenSource? _cts;

        public UcWarehouseOp(IStorePalletUseCase store, IRetrievePalletUseCase retrieve,
            IResetOutputsUseCase reset, IClearAllSlotsUseCase clearSlots)
        {
            InitializeComponent();
            _store = store;
            _retrieve = retrieve;
            _reset = reset;
            _clearSlots = clearSlots;

            cmbMode.SelectedIndex = 0;

        }

        private void AppendLog(string msg, Color color)
        {
            if (txtLog.IsDisposed) return;
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action<string, Color>(AppendLog), msg, color);
                return;
            }
            txtLog.SelectionStart = txtLog.TextLength;
            txtLog.SelectionLength = 0;
            txtLog.SelectionColor = color;

            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
            txtLog.SelectionColor = Color.Black;

            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }

        private void AppendLog(string msg)
        {
            AppendLog(msg, Color.Black);
        }

        private async void BtnStartOperation_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;

            try
            {
                _cts?.Dispose();
                _cts = new CancellationTokenSource();

                var slotNumber = (int)numSlot.Value;
                var progress = new Progress<string>(msg => AppendLog(msg));

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

                AppendLog(ok ? "Completed successfully." : "Completed with errors.", ok ? Color.Green : Color.DarkRed);
            }
            catch (OperationCanceledException)
            {
                AppendLog("Operation canceled.", Color.DarkOrange);
            }
            catch (Exception ex)
            {
                AppendLog("Error: " + ex.Message, Color.DarkRed);
            }
            finally
            {
                btnStart.Enabled = true;
            }
        }

        private async void BtnQuitterung_Click(object sender, EventArgs e)
        {
            AppendLog("Quitterung clicked");

            btnQuitterung.Enabled = false;

            try
            {
                var progress = new Progress<string>(msg => AppendLog(msg));

                await _reset.RunAsync(progress, _cts?.Token ?? CancellationToken.None);
                AppendLog("✅ All outputs reset successfully.", Color.Green);
            }
            catch (Exception ex)
            {
                AppendLog($"❌ Quitterung failed: {ex.Message}", Color.DarkRed);
            }
            finally
            {
                btnQuitterung.Enabled = true;
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        private async void btnClearInventory_Click(object sender, EventArgs e)
        {
            AppendLog("Clearing all inventory slots…");

            try
            {
                await _clearSlots.RunAsync();
                AppendLog("All slots marked as EMPTY.", Color.LimeGreen);
            }
            catch (Exception ex)
            {
                AppendLog($"Error clearing slots: {ex.Message}", Color.Red);
            }
        }
    }
}
