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

        void Update()
        {
            if (!isLocalPlayer) return;

            var h = Input.GetAxisRaw("Horizontal");
            var v = Input.GetAxisRaw("Vertical");

            var inputDirection = new Vector3(h, 0, v);

            if (inputDirection != Vector3.zero)
            {
                _movementSpeed = 1;
                _direction = inputDirection.normalized;
            }
            else if (_movementSpeed > 0)
            {
                _movementSpeed -= movementGravity;
            }

            var tr = transform;
            
            if (_movementSpeed > 0)
            {
                rb.velocity = _direction * (movementMaxSpeed);
            }
            else
            {
                rb.velocity = Vector3.zero;
            }

            if (_direction != Vector3.zero)
            {
                tr.rotation = Quaternion.RotateTowards(tr.rotation, Quaternion.LookRotation(_direction),
                    rotationSpeed * Time.deltaTime);
            }
        }
    }
}