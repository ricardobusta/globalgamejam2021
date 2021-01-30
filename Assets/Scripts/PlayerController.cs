using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace GameJam
{
    public class PlayerController : NetworkBehaviour
    {
        public float rotationSpeed;
        public float movementSpeed;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!isLocalPlayer) return;

            //var 
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            // var h = Input.GetAxisRaw("Horizontal");
            // var h = Input.GetAxisRaw("Vertical");
            
            var direction = new Vector3(h, 0, v);

            var directionMagnitude = direction.sqrMagnitude;
            if (directionMagnitude > 1)
            {
                direction = direction.normalized;
            }

            if (direction != Vector3.zero)
            {
                var tr = transform;

                tr.position += direction * (movementSpeed * Time.deltaTime);
                tr.forward = direction;
            }
        }
    }
}