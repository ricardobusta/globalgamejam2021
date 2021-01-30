using System;
using GameJam;
using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameNetworkManager : NetworkManager
{
    private static readonly Color[] _colors = {Color.red, Color.blue, Color.green, Color.yellow, Color.black, Color.white};

    public event Action ServerStartedEvent;
    public event Action ServerStoppedEvent;
    public event Action ClientStartedEvent;
    public event Action ClientStoppedEvent;
    public event Action HostStartedEvent;
    public event Action HostStoppedEvent;
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        
        NetworkServer.RegisterHandler<CreatePlayerMessage>(OnCreatePlayer);
        
        ServerStartedEvent?.Invoke();
    }
    
    public override void OnStopServer()
    {
        base.OnStopServer();
        ServerStoppedEvent?.Invoke();
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

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        ClientStartedEvent?.Invoke();
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        ClientStoppedEvent?.Invoke();
    }

    public override void OnStartHost()
    {
        base.OnStartHost();
        HostStartedEvent?.Invoke();
    }

    public override void OnStopHost()
    {
        base.OnStopHost();
        HostStoppedEvent?.Invoke();
    }
}
