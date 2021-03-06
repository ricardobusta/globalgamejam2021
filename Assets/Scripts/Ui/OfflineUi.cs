﻿using Mirror.SimpleWeb;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam
{
    public class OfflineUi : MonoBehaviour
    {
        public Button hostAndClientButton;
        public Button clientButton;
        public Button serverButton;
        public TMP_InputField hostAddress;
        public TMP_InputField hostPort;

        private const ushort DEFAULT_PORT = 7778;

        private void Start()
        {
            var networkManager = FindObjectOfType<MyNetworkRoomManager>();
            var transport = FindObjectOfType<SimpleWebTransport>();

            hostAddress.SetTextWithoutNotify(networkManager.networkAddress);
            hostPort.SetTextWithoutNotify(transport.port.ToString());

            hostAndClientButton.onClick.AddListener(() =>
            {
                transport.port = GetPort(hostPort.text);
                networkManager.StartHost();
            });
            
#if UNITY_WEBGL
            hostAndClientButton.gameObject.SetActive(false);
#endif

            clientButton.onClick.AddListener(() =>
            {
                networkManager.networkAddress = hostAddress.text;
                transport.port = GetPort(hostPort.text);
                networkManager.StartClient();
            });

            if (serverButton != null)
            {
                serverButton.onClick.AddListener(() =>
                {
                    transport.port = GetPort(hostPort.text);
                    networkManager.StartServer();
                });
            }
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