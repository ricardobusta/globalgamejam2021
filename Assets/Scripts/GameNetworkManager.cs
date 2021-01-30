using Mirror;

public class GameNetworkManager : NetworkManager
{
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        ClientScene.AddPlayer(conn);
    }
}
