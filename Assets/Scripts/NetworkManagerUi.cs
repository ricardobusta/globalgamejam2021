using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam
{
    public class NetworkManagerUi : MonoBehaviour
    {
        public NetworkManager networkManager;

        [Header("Connection Interface")]
        public Button hostAndClientButton;
        public Button clientButton;
        public Button serverButton;
        public TMP_InputField hostAddress;

        [Header("Info Interface")] 
        public TMP_Text serverInfo;
        public Button disconnectButton;
        
        [Header("Canvases")]
        public Canvas networkConnectCanvas;
        public Canvas networkInfoCanvas;

        private void Start()
        {
            hostAddress.SetTextWithoutNotify(networkManager.networkAddress);
            
            ToggleCanvas(false);
            hostAndClientButton.onClick.AddListener(() =>
            {
                networkManager.StartHost();
                ToggleCanvas(true);
            });
            
            clientButton.onClick.AddListener(() =>
            {
                networkManager.networkAddress = hostAddress.text;
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

        private void UpdateInfo()
        {
            serverInfo.text = $"Transport: {Transport.activeTransport}\nAddress: {networkManager.networkAddress}";
        }

        private void ToggleCanvas(bool info)
        {
            networkConnectCanvas.gameObject.SetActive(!info);
            networkInfoCanvas.gameObject.SetActive(info);
            UpdateInfo();
        }
    }
}