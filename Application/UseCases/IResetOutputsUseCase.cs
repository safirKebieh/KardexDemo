namespace Application.UseCases
{
    public interface IResetOutputsUseCase
    {
        Task RunAsync(IProgress<string>? progress, CancellationToken ct);
    }
}
