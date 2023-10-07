using Fusion;

public struct NetworkInputData: INetworkInput
{
    public float xMovement;
    public float zMovement;
    public NetworkBool isUsingItem;
    public NetworkBool hasItem;
    public NetworkBool isBeingBoosted;
}
