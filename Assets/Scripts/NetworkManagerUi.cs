using Mirror;
using Mirror.SimpleWeb;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam
{
    public class NetworkManagerUi : MonoBehaviour
    {
        [Header("Network")]
        public NetworkManager networkManager;
        public SimpleWebTransport transport;

        [Header("Connection Interface")]
        public Button hostAndClientButton;
        public Button clientButton;
        public Button serverButton;
        public TMP_InputField hostAddress;
        public TMP_InputField hostPort;

        [Header("Info Interface")] 
        public TMP_Text serverInfo;
        public Button disconnectButton;
        
        [Header("Canvases")]
        public Canvas networkConnectCanvas;
        public Canvas networkInfoCanvas;

        private const ushort DEFAULT_PORT = 7778;

        private void Start()
        {
            hostAddress.SetTextWithoutNotify(networkManager.networkAddress);
            
            ToggleCanvas(false);
            hostAndClientButton.onClick.AddListener(() =>
            {
                transport.port = GetPort(hostPort.text);
                networkManager.StartHost();
                ToggleCanvas(true);
            });
            
            clientButton.onClick.AddListener(() =>
            {
                networkManager.networkAddress = hostAddress.text;
                transport.port = GetPort(hostPort.text);
                networkManager.StartClient();
                ToggleCanvas(true);
            });
            
            serverButton.onClick.AddListener(() =>
            {
                networkManager.StartServer();
                ToggleCanvas(true);
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
        }

        private ushort GetPort(string portString)
        {
            if (ushort.TryParse(portString, out var port))
            {
                return port;
            }

            return DEFAULT_PORT;
        }
        
        private void UpdateInfo()
        {
            serverInfo.text = $"Transport: {Transport.activeTransport}\nAddress: {networkManager.networkAddress}:{transport.port}";
        }

        private void ToggleCanvas(bool info)
        {
            networkConnectCanvas.gameObject.SetActive(!info);
            networkInfoCanvas.gameObject.SetActive(info);
            UpdateInfo();
        }
    }
}