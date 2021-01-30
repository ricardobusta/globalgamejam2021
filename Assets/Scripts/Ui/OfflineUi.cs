using Mirror.SimpleWeb;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam
{
    public class OfflineUi : MonoBehaviour
    {
        [Header("Connection Interface")] public Button hostAndClientButton;
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
        }

        private ushort GetPort(string portString)
        {
            if (ushort.TryParse(portString, out var port))
            {
                return port;
            }

            return DEFAULT_PORT;
        }
    }
}