using UnityEngine;

namespace GameJam
{
    public struct Movement
    {
        public Quaternion Rotation { get; }
        public Vector3 Velocity { get; }

        public Movement(Quaternion rotation, Vector3 velocity)
        {
            Rotation = rotation;
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
            var inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (inputDirection.sqrMagnitude > float.Epsilon)
            {
                _direction = inputDirection.normalized;
                _movementSpeed = Mathf.Min(1, _movementSpeed + _acceleration);
            }
            else if (_movementSpeed > 0)
            {
                _movementSpeed = Mathf.Max(0, _movementSpeed - _acceleration);
            }

            var rotation = transform.rotation;
            var rotate = _direction != Vector3.zero
                ? Quaternion.RotateTowards(rotation, Quaternion.LookRotation(_direction),
                    _rotationSpeed * deltaTime)
                : rotation;
            return new Movement(rotate, _direction * (_movementSpeed * _maxSpeed * deltaTime));
        }
    }
}