namespace Application.StorageProcess;

public enum StorageState
{
    Idle,
    Infeed,          
    PalletToCrane,      
    CraneHandshake,  
    UpdateInventory, 
    NoFreeSlots,     
    Fault            
}
