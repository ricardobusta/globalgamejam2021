using UnityEngine;

namespace GameJam
{
    public struct Movement
    {
        public Vector3 Direction { get; }
        public Vector3 Velocity { get; }

        public Movement(Vector3 direction, Vector3 velocity)
        {
            Direction = direction;
            Velocity = velocity;
        }
    }

    public class BodyMovement : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _rotationSpeed;
        [Range(0f, 1f)] [SerializeField] private float _acceleration;

        private float _movementSpeed;
        private Vector3 _direction;

        public Movement ProcessUpdate(float deltaTime)
        {
            var inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            if (inputDirection.sqrMagnitude > float.Epsilon)
            {
                _direction = inputDirection.normalized;
                _movementSpeed = Mathf.Min(1, _movementSpeed + _acceleration);
            }
            else
            {
                _movementSpeed = Mathf.Max(0, _movementSpeed - _acceleration);
            }

            if (_movementSpeed <= Mathf.Epsilon)
            {
                _movementSpeed = 0;
            }

            Vector3 movement = Vector3.zero;
            if (_movementSpeed > Mathf.Epsilon)
            {
                movement = _direction * _movementSpeed * _maxSpeed * deltaTime;
            }

            return new Movement(_direction, movement);
        }
    }
}