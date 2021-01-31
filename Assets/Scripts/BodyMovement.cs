using UnityEngine;

namespace GameJam
{
    public class BodyMovement : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _rotationSpeed;
        [Range(0f, 1f)] [SerializeField] private float _acceleration;

        private float _movementSpeed;
        private Vector3 _direction;
        private Vector3 _lookDirection = Vector3.up;

        public (Vector3 velocity, Vector3 lookDirection) ProcessUpdate(float deltaTime)
        {
            var inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            if (inputDirection.sqrMagnitude > float.Epsilon)
            {
                _direction = inputDirection.normalized;
                _movementSpeed = Mathf.Min(1, _movementSpeed + _acceleration);
                _lookDirection = Vector3.RotateTowards(_lookDirection, _direction, _rotationSpeed * deltaTime, 0);
            }
            else
            {
                _movementSpeed = Mathf.Max(0, _movementSpeed - _acceleration);
            }

            return (_direction * _movementSpeed * _maxSpeed * deltaTime, _lookDirection);
        }
    }
}