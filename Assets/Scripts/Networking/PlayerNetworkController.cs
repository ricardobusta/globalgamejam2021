using Mirror;
using UnityEngine;

namespace GameJam
{
    public class PlayerNetworkController : NetworkBehaviour
    {
        [SerializeField] private BodyMovement _bodyMovement;
        [SerializeField] private Rigidbody2D _rigidBody;

        public SpriteRenderer spriteRenderer;
        public Animator animator;
        public RuntimeAnimatorController[] playerAnimator;
        
        [SyncVar(hook = nameof(PlayerColorChanged))]
        public Color color;
        
        [SyncVar(hook = nameof(PlayerIndexChanged))]
        public int index;

        private void FixedUpdate()
        {
            if (!isLocalPlayer) return;

            var movement = _bodyMovement.ProcessUpdate(Time.fixedDeltaTime);
            _rigidBody.velocity = movement.velocity;
        }

        public void PlayerColorChanged(Color _, Color newValue)
        {
        }
        
        public void PlayerIndexChanged(int _, int newValue)
        {
            animator.runtimeAnimatorController = playerAnimator[newValue];
        }
    }
}