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

            Quaternion rotation = transform.rotation;
            Quaternion rotate = _direction != Vector3.zero
                ? Quaternion.RotateTowards(rotation, Quaternion.LookRotation(Vector3.forward, _direction),
                    _rotationSpeed * deltaTime)
                : rotation;

            Vector3 movement = Vector3.zero;
            if (_movementSpeed > Mathf.Epsilon)
            {
                movement = _direction * _movementSpeed * _maxSpeed * deltaTime;
            }

            return new Movement(rotate, movement);
        }
    }
}