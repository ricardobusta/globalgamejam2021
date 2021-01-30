using Mirror;
using UnityEngine;

namespace GameJam
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private BodyMovement _bodyMovement;
        [SerializeField] private Rigidbody2D _rigidBody;

        public SpriteRenderer playerSprite;

        public Color color;

        public override void OnStartClient()
        {
            base.OnStartClient();
            playerSprite.color = color;
        }

        public void LateUpdate()
        {
            playerSprite.transform.rotation = Quaternion.identity;
        }

        private void FixedUpdate()
        {
            if (!isLocalPlayer) return;

            var movement = _bodyMovement.ProcessUpdate(Time.fixedDeltaTime);
            _rigidBody.velocity = movement.velocity;
        }
    }
}