using Mirror;
using UnityEngine;

namespace GameJam
{
    public class PlayerController : NetworkBehaviour
    {
        public float rotationSpeed;
        public float movementMaxSpeed;
        public float movementGravity;

        private float _movementSpeed;
        private Vector3 _direction;

        public Rigidbody rb;

        [SyncVar] public Color playerColor;
        private Color previousColor;

        public Renderer renderer;

        private static readonly Color[] PlayerColors = new[]
        {
            Color.red,
            Color.blue,
            Color.green,
            Color.cyan,
            Color.yellow,
            Color.black,
            Color.white,
        };

        private void Start()
        {
            if (isLocalPlayer)
            {
                playerColor = PlayerColors[Random.Range(0, PlayerColors.Length)];
                previousColor = playerColor;
            }
        }

        private void Update()
        {
            if (previousColor != playerColor)
            {
                var material = new Material(renderer.material) {color = playerColor};
                renderer.material = material;
                previousColor = playerColor;
            }
        }

        private void FixedUpdate()
        {
            if (!isLocalPlayer) return;

            var h = Input.GetAxisRaw("Horizontal");
            var v = Input.GetAxisRaw("Vertical");

            var inputDirection = new Vector3(h, 0, v);

            if (inputDirection.sqrMagnitude > float.Epsilon)
            {
                _direction = inputDirection.normalized;
                _movementSpeed = Mathf.Min(1, _movementSpeed + movementGravity);
            }
            else if (_movementSpeed > 0)
            {
                _movementSpeed = Mathf.Max(0, _movementSpeed - movementGravity);
            }

            var tr = transform;

            rb.velocity = _direction * _movementSpeed * movementMaxSpeed * Time.fixedDeltaTime;

            tr.rotation = Quaternion.RotateTowards(
                tr.rotation,
                Quaternion.LookRotation(_direction),
                rotationSpeed * Time.fixedDeltaTime
            );
        }
    }
}