namespace Application.Warehouse.Io;

public sealed record InputSnapshot(
    bool AtEntry,          // box at infeed sensor
    bool AtLoad,           // box at crane load point
    bool CraneReady,       // crane ready for command
    bool CraneBusy,        // crane moving/processing
    bool CraneDone,        // last command finished
    bool EmergencyStop,    // E-Stop
    bool Fault,             // general fault
    bool CraneLeftLimit,     // Input 2
    bool CraneMiddleLimit,   // Input 3
     bool CraneRightLimit,   // Input 4
    bool MovingX,            // Input 7
    bool MovingZ,             // Input 8
    bool AtExit                 // Input 6
);
