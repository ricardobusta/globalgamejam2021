using Mirror;
using Mirror.SimpleWeb;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam
{
    public class RoomUi : MonoBehaviour
    {
        public TMP_Text serverInfo;
        public Button stopButton;
        public Button readyButton;
        public Button startGameButton;

        public Transform roomPlayerRoot;

        private MyNetworkRoomManager _networkManager;
        
        private void Start()
        {
            _networkManager = FindObjectOfType<MyNetworkRoomManager>();
            var transport = FindObjectOfType<SimpleWebTransport>();
            
            serverInfo.text = $"Transport: {Transport.activeTransport}\nAddress: {_networkManager.networkAddress}:{transport.port}";
            
            startGameButton.onClick.AddListener(_networkManager.OnStartButtonClicked);
            
            SetupStopButton();
            UpdateReadyStatus();
        }

        private void SetupStopButton()
        {
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                stopButton.GetComponentInChildren<TMP_Text>().text = "Stop Host";
                stopButton.onClick.AddListener(_networkManager.StopHost);
            }
            else if (NetworkClient.isConnected)
            {
                stopButton.GetComponentInChildren<TMP_Text>().text = "Stop Client";
                stopButton.onClick.AddListener(_networkManager.StopClient);
            }
            else if (NetworkServer.active)
            {
                stopButton.GetComponentInChildren<TMP_Text>().text = "Stop Server";
                stopButton.onClick.AddListener(_networkManager.StopServer);
            }
        }

        public void UpdateReadyStatus()
        {
            startGameButton.gameObject.SetActive(_networkManager.ShouldShowStartButton());
        }
    }
}