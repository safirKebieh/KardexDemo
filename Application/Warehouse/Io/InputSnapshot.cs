namespace Application.Warehouse.Io;

public sealed record InputSnapshot(
    bool AtEntry,          
    bool AtLoad,           
    bool CraneReady,       
    bool CraneBusy,        
    bool CraneDone,        
    bool EmergencyStop,    
    bool Fault,            
    bool CraneLeftLimit,   
    bool CraneMiddleLimit, 
     bool CraneRightLimit, 
    bool MovingX,          
    bool MovingZ,          
    bool AtExit            
);
