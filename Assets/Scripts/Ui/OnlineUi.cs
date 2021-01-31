using UnityEngine;

namespace GameJam
{
    public class OnlineUi : MonoBehaviour
    {
        private void Start()
        {
            var networkManager = FindObjectOfType<GameNetworkManager>();
        }
    }
}