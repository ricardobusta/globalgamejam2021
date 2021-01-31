using System;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam
{
    public class MyNetworkRoomPlayer : NetworkRoomPlayer
    {
        [Header("References")] 
        public TMP_Text playerNameLabel;
        public TMP_Text playerIdLabel;
        public TMP_Text playerReadyLabel;

        public Button playerReadyButton;

        [Header("Player Identity")]
        public string playerName;

        private bool _isMyPlayer;
        private bool _previouslyReady;

        private bool _started;

        private new void Start()
        {
            base.Start();
            var roomUi = FindObjectOfType<RoomUi>();

            var tr = transform;
            tr.SetParent(roomUi.roomPlayerRoot);
            tr.localScale = Vector3.one;
            tr.position = Vector3.zero;

            playerNameLabel.text = playerName;
            playerIdLabel.text = $"Player {index + 1}";
            UpdateReadyLabel();

            _isMyPlayer = NetworkClient.active && isLocalPlayer;
            
            playerReadyButton.gameObject.SetActive(_isMyPlayer);
            var readyButtonLabel = playerReadyButton.GetComponentInChildren<TMP_Text>();
            UpdateButtonLabel(readyButtonLabel);
            playerReadyButton.onClick.AddListener(()=>
            {
                UpdateButtonLabel(readyButtonLabel);
                CmdChangeReadyState(!readyToBegin);
            });

            _started = true;
        }

        private void UpdateButtonLabel(TMP_Text readyButtonLabel)
        {
            readyButtonLabel.text = readyToBegin ? "Ready" : "Cancel";
        }

        private void UpdateReadyLabel()
        {
            playerReadyLabel.text = readyToBegin ? "Ready" : "Not Ready";
        }

        private void Update()
        {
            if (!_started) return;
            
            if (_previouslyReady!=readyToBegin)
            {
                _previouslyReady = readyToBegin;
                UpdateReadyLabel();
            }
        }
    }
}