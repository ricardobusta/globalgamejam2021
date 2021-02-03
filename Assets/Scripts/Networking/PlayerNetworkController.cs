using Cinemachine;
using Mirror;
using UnityEngine;

namespace GameJam
{
    public class PlayerNetworkController : NetworkBehaviour
    {
        private BodyMovement _bodyMovement;

        public SpriteRenderer spriteRenderer;
        public Animator animator;
        public RuntimeAnimatorController[] playerAnimator;
        
        [SyncVar(hook = nameof(PlayerIndexChanged))]
        public int index;

        private void Awake()
        {
            _bodyMovement = GetComponent<BodyMovement>();
            _bodyMovement.enabled = false;
            
            animator.runtimeAnimatorController = playerAnimator[0];
        }

        public override void OnStartLocalPlayer()
        {
            _bodyMovement.enabled = true;
            FindObjectOfType<CinemachineVirtualCamera>().Follow = transform;
            var lightTransform = FindObjectOfType<LightController>().transform;
            lightTransform.SetParent(transform);
            lightTransform.localPosition = Vector3.zero;
            
            _bodyMovement.SetLightTransform(lightTransform);
        }

        public void PlayerIndexChanged(int _, int newValue)
        {
            Debug.Log(newValue);
            index = newValue;
            animator.runtimeAnimatorController = playerAnimator[newValue];

            if (newValue > 0)
            {
                gameObject.layer = LayerMask.NameToLayer("HumanPlayer");
            }
        }
    }
}