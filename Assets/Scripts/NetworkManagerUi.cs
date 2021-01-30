using Mirror;
using Mirror.SimpleWeb;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam
{
    public class NetworkManagerUi : MonoBehaviour
    {
        [Header("Connection Interface")]
        public Button hostAndClientButton;
        public Button clientButton;
        public TMP_InputField hostAddress;
        public TMP_InputField hostPort;

        private const ushort DEFAULT_PORT = 7778;

        private void Start()
        {
            var networkManager = FindObjectOfType<GameNetworkManager>();
            var transport = FindObjectOfType<SimpleWebTransport>();
            
            hostAddress.SetTextWithoutNotify(networkManager.networkAddress);
            
            hostAndClientButton.onClick.AddListener(() =>
            {
                transport.port = GetPort(hostPort.text);
                networkManager.StartHost();
            });
            
            clientButton.onClick.AddListener(() =>
            {
                networkManager.networkAddress = hostAddress.text;
                transport.port = GetPort(hostPort.text);
                networkManager.StartClient();
            });
            
            disconnectButton.onClick.AddListener(() =>
            {
                if (NetworkClient.isConnected)
                {
                    if (NetworkServer.active)
                    {
                        networkManager.StopHost();
                    }
                    else
                    {
                        networkManager.StopClient();
                    }
                }
                else
                {
                    if (NetworkServer.active)
                    {
                        networkManager.StopServer();
                    }
                }
                ToggleCanvas(false);
            });
            
            startGameButton.onClick.AddListener(() =>
            {
                // if (networkManager.TryStartGame())
                // {
                //     startGameCanvas.gameObject.SetActive(false);
                // }
            });

            //networkManager.ClientStartedEvent += () => { ToggleCanvas(true); };
            //networkManager.ServerStartedEvent += () => { ToggleCanvas(true); };
            //networkManager.HostStartedEvent += () => { ToggleCanvas(true); };
            
            //networkManager.ClientStoppedEvent += () => { ToggleCanvas(false); };
            //networkManager.ServerStoppedEvent += () => { ToggleCanvas(false); };
            //networkManager.HostStoppedEvent += () => { ToggleCanvas(false); };
        }

        private ushort GetPort(string portString)
        {
            if (ushort.TryParse(portString, out var port))
            {
                return port;
            }

            return DEFAULT_PORT;
        }
        
        //
        // private void UpdateInfo()
        // {
        //     serverInfo.text = $"Transport: {Transport.activeTransport}\nAddress: {networkManager.networkAddress}:{transport.port}";
        // }
    }
}