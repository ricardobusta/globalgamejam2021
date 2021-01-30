using GameJam;
using Mirror;
using UnityEngine;

public class GameNetworkManager : NetworkManager
{
    private static readonly Color[] _colors = {Color.red, Color.blue, Color.green, Color.yellow, Color.black, Color.white};
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        
        NetworkServer.RegisterHandler<CreatePlayerMessage>(OnCreatePlayer);
    }

    private void OnCreatePlayer(NetworkConnection conn, CreatePlayerMessage message)
    {
        var go = Instantiate(playerPrefab);

        var pc = go.GetComponent<PlayerController>();

        if (pc != null)
        {
            pc.color = message.color;
        }

        NetworkServer.AddPlayerForConnection(conn, go);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        var playerMessage = new CreatePlayerMessage()
        {
            color = _colors[Random.Range(0, _colors.Length)]
        };
        
        conn.Send(playerMessage);
    }
}
