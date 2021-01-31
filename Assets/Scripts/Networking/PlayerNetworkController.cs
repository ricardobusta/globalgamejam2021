using Mirror;
using UnityEngine;

namespace GameJam
{
    public class PlayerNetworkController : NetworkBehaviour
    {
        [SerializeField] private BodyMovement _bodyMovement;
        [SerializeField] private Rigidbody2D _rigidBody;

        public SpriteRenderer spriteRenderer;
        
        [SyncVar]
        public Color color;

        private void FixedUpdate()
        {
            spriteRenderer.color = color;
            
            if (!isLocalPlayer) return;

            var movement = _bodyMovement.ProcessUpdate(Time.fixedDeltaTime);
            _rigidBody.velocity = movement.velocity;
        }
    }
}