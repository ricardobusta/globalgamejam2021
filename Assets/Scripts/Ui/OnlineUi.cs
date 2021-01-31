using UnityEngine;
using UnityEngine.UI;

namespace GameJam
{
    public class OnlineUi : MonoBehaviour
    {
        public Button returnToRoomButton;
        private void Start()
        {
            var networkManager = FindObjectOfType<MyNetworkRoomManager>();
            
            returnToRoomButton.onClick.AddListener(()=>networkManager.ServerChangeScene(networkManager.RoomScene));
        }
    }
}