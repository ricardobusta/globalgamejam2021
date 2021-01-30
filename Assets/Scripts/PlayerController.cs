using System.Collections;
using System.Collections.Generic;
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

            rb.velocity = _direction * _movementSpeed * movementMaxSpeed * Time.deltaTime;

            tr.rotation = Quaternion.RotateTowards(
                tr.rotation,
                Quaternion.LookRotation(_direction),
                rotationSpeed * Time.deltaTime
            );
        }
    }
}