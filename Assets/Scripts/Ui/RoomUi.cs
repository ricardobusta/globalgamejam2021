using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam
{
    public class RoomUi : MonoBehaviour
    {
        public Button stopButton;
        
        private void Start()
        {
            var networkManager = FindObjectOfType<GameNetworkManager>();

            SetupStopButton(networkManager);
        }

        private void SetupStopButton(NetworkManager networkManager)
        {
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                stopButton.GetComponentInChildren<TMP_Text>().text = "Stop Host";
                stopButton.onClick.AddListener(networkManager.StopHost);
            }
            else if (NetworkClient.isConnected)
            {
                stopButton.GetComponentInChildren<TMP_Text>().text = "Stop Client";
                stopButton.onClick.AddListener(networkManager.StopClient);
            }
            else if (NetworkServer.active)
            {
                stopButton.GetComponentInChildren<TMP_Text>().text = "Stop Server";
                stopButton.onClick.AddListener(networkManager.StopServer);
            }
        }
    }
}