using System;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam
{
    public class MyNetworkRoomPlayer : NetworkRoomPlayer
    {
        private static readonly Color[] PlayerColors =
        {
            Color.red,
            Color.blue,
            Color.green,
            Color.yellow,
            Color.white,
        };

        [Header("References")] 
        public Canvas tempCanvas;
        public GameObject rootObject;
        public TMP_Text playerNameLabel;
        public TMP_Text playerIdLabel;
        public TMP_Text playerReadyLabel;
        public Button kickButton;
        public Image playerImage;
        
        [Header("Player Identity")] 
        [SyncVar(hook = nameof(PlayerNameChanged))]
        public string playerName;
        [SyncVar(hook = nameof(PlayerColorChanged))]
        public Color playerColor;

        private bool _isMyPlayer;
        private bool _previouslyReady;

        private bool _started;

        private Action _updateReadyAction;

        private new void Start()
        {
            base.Start();
            var roomUi = FindObjectOfType<RoomUi>();

            _updateReadyAction = ()=>
            {
                if (roomUi != null)
                {
                    roomUi.UpdateReadyStatus();
                }
            };
            _isMyPlayer = NetworkClient.active && isLocalPlayer;

            var tr = rootObject.transform;
            tr.SetParent(roomUi.roomPlayerRoot);
            tr.localScale = Vector3.one;
            tr.position = Vector3.zero;
            
            playerColor = PlayerColors[index % PlayerColors.Length];
            
            tempCanvas.gameObject.SetActive(false);

            playerIdLabel.text = $"Player {index + 1}";
            UpdateReadyLabel();
            
            var showKickButton = (isServer && index > 0) || isServerOnly;
            kickButton.gameObject.SetActive(showKickButton);
            kickButton.onClick.AddListener(() => { GetComponent<NetworkIdentity>().connectionToClient.Disconnect(); });

            if (_isMyPlayer)
            {
                var readyButtonLabel = roomUi.readyButton.GetComponentInChildren<TMP_Text>();
                UpdateButtonLabel(readyButtonLabel, false);
                roomUi.readyButton.onClick.AddListener(() =>
                {
                    UpdateButtonLabel(readyButtonLabel, !readyToBegin);
                    CmdChangeReadyState(!readyToBegin);
                });
            }
            
            _started = true;
        }

        public void PlayerNameChanged(string _, string newName)
        {
            playerNameLabel.text = newName;
        }

        public void PlayerColorChanged(Color _, Color newColor)
        {
            playerImage.color = newColor;
        }

        private void OnDestroy()
        {
            if (rootObject != null)
            {
                Destroy(rootObject);
            }
        }

        private void UpdateButtonLabel(TMP_Text readyButtonLabel, bool ready)
        {
            readyButtonLabel.text = !ready ? "Ready" : "Cancel";
        }

        private void UpdateReadyLabel()
        {
            playerReadyLabel.text = readyToBegin ? "Ready" : "Not Ready";
        }

        private void Update()
        {
            if (!_started) return;

            if (_previouslyReady != readyToBegin)
            {
                _previouslyReady = readyToBegin;
                _updateReadyAction?.Invoke();
                UpdateReadyLabel();
            }
        }
    }
}