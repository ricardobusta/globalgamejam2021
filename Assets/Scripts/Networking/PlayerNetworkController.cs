using Mirror;
using UnityEngine;

namespace GameJam
{
    public class PlayerNetworkController : NetworkBehaviour
    {
        [SerializeField] private BodyMovement _bodyMovement;
        [SerializeField] private Rigidbody2D _rigidBody;

        public SpriteRenderer spriteRenderer;
        
        [SyncVar(hook = nameof(PlayerColorChanged))]
        public Color color;

        private void FixedUpdate()
        {
            if (!isLocalPlayer) return;

            var movement = _bodyMovement.ProcessUpdate(Time.fixedDeltaTime);
            _rigidBody.velocity = movement.velocity;
        }

        public void PlayerColorChanged(Color _, Color newValue)
        {
            spriteRenderer.color = newValue;
        }
    }
}