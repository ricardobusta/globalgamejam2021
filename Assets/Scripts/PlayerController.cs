using Mirror;
using UnityEngine;

namespace GameJam
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private BodyMovement _bodyMovement;
        [SerializeField] private Rigidbody2D _rigidBody;

        public Color color;

        public override void OnStartClient()
        {
            base.OnStartClient();
            var rend = GetComponent<SpriteRenderer>();
            rend.color = color;
        }

        private void FixedUpdate()
        {
            if (!isLocalPlayer) return;

            Movement movement = _bodyMovement.ProcessUpdate(Time.deltaTime);
            _rigidBody.velocity = movement.Velocity;
            transform.rotation = movement.Rotation;
        }
    }
}