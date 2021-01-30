using Mirror;
using UnityEngine;

namespace GameJam
{
    [RequireComponent(typeof(BodyMovement)), RequireComponent(typeof(Rigidbody))]
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private BodyMovement _bodyMovement;
        [SerializeField] private Rigidbody _rigidBody;

        public Color color;

        private void Start()
        {
            var rend = GetComponent<Renderer>();
            var mat = new Material(rend.material) {color = color};
            rend.material = mat;
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