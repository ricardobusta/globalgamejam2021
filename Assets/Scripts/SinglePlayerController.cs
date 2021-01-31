using UnityEngine;

namespace GameJam
{
    [RequireComponent(typeof(BodyMovement)), RequireComponent(typeof(Rigidbody2D))]
    public class SinglePlayerController : MonoBehaviour
    {
        [SerializeField] private BodyMovement _bodyMovement;
        [SerializeField] private Rigidbody2D _rigidBody;

        private void FixedUpdate()
        {
            var movement = _bodyMovement.ProcessUpdate(Time.fixedDeltaTime);
            _rigidBody.velocity = movement.velocity;
        }
    }
}