using UnityEngine;

namespace GameJam
{
    [RequireComponent(typeof(BodyMovement)), RequireComponent(typeof(Rigidbody))]
    public class SinglePlayerController : MonoBehaviour
    {
        [SerializeField] private BodyMovement _bodyMovement;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private FieldOfView _fov;

        private void FixedUpdate()
        {
            var movement = _bodyMovement.ProcessUpdate(Time.deltaTime);
            _rigidBody.velocity = movement.velocity;
            _fov.SetDirection(movement.lookDirection);
        }
    }
}