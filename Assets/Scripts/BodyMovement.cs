using UnityEngine;

namespace GameJam
{
    public class BodyMovement : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _acceleration;
        
        private Rigidbody2D _rigidBody;

        private Transform _lightTransform;
        
        private float _movementSpeed;
        private Vector3 _direction = Vector3.down;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        public void SetLightTransform(Transform light)
        {
            _lightTransform = light;
        }

        private void FixedUpdate()
        {
            var inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            if (inputDirection.sqrMagnitude > float.Epsilon)
            {
                _direction = inputDirection.normalized;
                _movementSpeed += _acceleration * Time.fixedDeltaTime;
            }
            else
            {
                _movementSpeed -= _acceleration * Time.fixedDeltaTime;
            }

            if ((_lightTransform.up - _direction).sqrMagnitude > float.Epsilon)
            {
                _lightTransform.up = Vector3.RotateTowards(_lightTransform.up, _direction, 
                    _rotationSpeed * Time.fixedDeltaTime, 0);
            }

            _movementSpeed = Mathf.Clamp(_movementSpeed, 0, _maxSpeed);

            _rigidBody.velocity = _direction * _movementSpeed;
        }
    }
}