using System;
using GameJam;
using Mirror;
using Mirror.Examples.NetworkRoom;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyNetworkRoomManager : NetworkRoomManager
{
      [Header("Spawner Setup")]
        [Tooltip("Reward Prefab for the Spawner")]
        public GameObject rewardPrefab;

        /// <summary>
        /// This is called on the server when a networked scene finishes loading.
        /// </summary>
        public override void OnRoomServerSceneChanged(string sceneName)
        {
            if (sceneName == GameplayScene)
            {
                //Spawner.InitialSpawn();
            }
        }

        /// <summary>
        /// Called just after GamePlayer object is instantiated and just before it replaces RoomPlayer object.
        /// This is the ideal point to pass any data like player name, credentials, tokens, colors, etc.
        /// into the GamePlayer object as it is about to enter the Online scene.
        /// </summary>
        public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnection conn, GameObject roomPlayer, GameObject gamePlayer)
        {
            //PlayerScore playerScore = gamePlayer.GetComponent<PlayerScore>();
            //playerScore.index = roomPlayer.GetComponent<NetworkRoomPlayer>().index;
            return true;
        }

        public override void OnRoomStopClient()
        {
            // Demonstrates how to get the Network Manager out of DontDestroyOnLoad when
            // going to the offline scene to avoid collision with the one that lives there.
            if (gameObject.scene.name == "DontDestroyOnLoad" && !string.IsNullOrEmpty(offlineScene) && SceneManager.GetActiveScene().path != offlineScene)
                SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());

            base.OnRoomStopClient();
        }

        public override void OnRoomStopServer()
        {
            // Demonstrates how to get the Network Manager out of DontDestroyOnLoad when
            // going to the offline scene to avoid collision with the one that lives there.
            if (gameObject.scene.name == "DontDestroyOnLoad" && !string.IsNullOrEmpty(offlineScene) && SceneManager.GetActiveScene().path != offlineScene)
                SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());

            base.OnRoomStopServer();
        }

        /*
            This code below is to demonstrate how to do a Start button that only appears for the Host player
            showStartButton is a local bool that's needed because OnRoomServerPlayersReady is only fired when
            all players are ready, but if a player cancels their ready state there's no callback to set it back to false
            Therefore, allPlayersReady is used in combination with showStartButton to show/hide the Start button correctly.
            Setting showStartButton false when the button is pressed hides it in the game scene since NetworkRoomManager
            is set as DontDestroyOnLoad = true.
        */

        bool showStartButton;

        public override void OnRoomServerPlayersReady()
        {
            // calling the base method calls ServerChangeScene as soon as all players are in Ready state.
#if UNITY_SERVER
            base.OnRoomServerPlayersReady();
#else
            showStartButton = true;
#endif
        }

        public override void OnGUI()
        {
            base.OnGUI();

            if (allPlayersReady && showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
            {
                // set to false to hide it in the game scene
                showStartButton = false;

                ServerChangeScene(GameplayScene);
            }
        }
    // private static readonly Color[] _colors = {Color.red, Color.blue, Color.green, Color.yellow, Color.black, Color.white};
    //
    //
    // public override void OnStartServer()
    // {
    //     base.OnStartServer();
    //     
    //     NetworkServer.RegisterHandler<CreatePlayerMessage>(OnCreatePlayer);
    //     
    //     ServerStartedEvent?.Invoke();
    // }
    //
    // public override void OnStopServer()
    // {
    //     base.OnStopServer();
    //     ServerStoppedEvent?.Invoke();
    // }
    //
    // private void OnCreatePlayer(NetworkConnection conn, CreatePlayerMessage message)
    // {
    //     var go = Instantiate(playerPrefab);
    //
    //     var pc = go.GetComponent<PlayerController>();
    //
    //     if (pc != null)
    //     {
    //         pc.color = message.color;
    //     }
    //
    //     NetworkServer.AddPlayerForConnection(conn, go);
    // }
    //
    // public override void OnClientConnect(NetworkConnection conn)
    // {
    //     base.OnClientConnect(conn);
    //
    //     var playerMessage = new CreatePlayerMessage()
    //     {
    //         color = _colors[Random.Range(0, _colors.Length)]
    //     };
    //     
    //     conn.Send(playerMessage);
    // }
    //
  
}
