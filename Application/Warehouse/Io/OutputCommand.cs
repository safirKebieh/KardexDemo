namespace Application.Warehouse.Io;

public sealed record OutputCommand(
    bool? InfeedConveyor = null,
    bool? ToCraneConveyor = null,
    bool? ExitConveyor = null,
    bool? CraneStart = null,
    bool? CraneLeft = null,
    bool? CraneLift = null,
    bool? CraneRight = null,
     bool? UnloadConveyor = null 
)
{
    public static OutputCommand None => new();
    public OutputCommand WithInfeed(bool value) => this with { InfeedConveyor = value };
    public OutputCommand WithToCrane(bool value) => this with { ToCraneConveyor = value };
    public OutputCommand WithExit(bool value) => this with { ExitConveyor = value };
    public OutputCommand WithUnload(bool v) => this with { UnloadConveyor = v };  

    public OutputCommand WithCraneStart(bool value) => this with { CraneStart = value };

    public OutputCommand WithCraneLeft(bool v) => this with { CraneLeft = v };
    public OutputCommand WithCraneLift(bool v) => this with { CraneLift = v };
    public OutputCommand WithCraneRight(bool v) => this with { CraneRight = v };

}
