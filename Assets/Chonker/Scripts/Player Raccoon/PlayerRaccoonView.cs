using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Chonker.Scripts.Player_Raccoon
{
    public class PlayerRaccoonView : MonoBehaviour
    {
        private PlayerRaccoonComponentContainer playerRaccoonComponentContainer;
        [SerializeField] private Animator _animator;
        [SerializeField] SpriteRenderer _spriteRenderer;

        private int animatorIsRunningHash;
        private int altIdleTriggerHash;

        private float altIdleTimer;
        private float altIdleTime = 10;

        private void Awake() {
            playerRaccoonComponentContainer = GetComponentInParent<PlayerRaccoonComponentContainer>();
            animatorIsRunningHash = Animator.StringToHash("IsRunning");
            altIdleTriggerHash = Animator.StringToHash("Alt Idle");
            GetComponentInChildren<CinemachineCamera>().transform.parent = null;
            GetComponentInChildren<Camera>().transform.parent = null;
        }

        private void Update() {
            bool isMoving = playerRaccoonComponentContainer.PlayerRaccoonController.Velocity.sqrMagnitude > .01f;
            _animator.SetBool(animatorIsRunningHash, isMoving);

            if (!isMoving) {
                altIdleTimer += Time.deltaTime;
            }
            else {
                altIdleTimer = 0;
            }

            if (altIdleTimer > altIdleTime) {
                _animator.SetTrigger(altIdleTriggerHash);
                altIdleTimer = 0;
            }
        }

        public void HideModel() {
            _spriteRenderer.enabled = false;
        }

        public void ShowModel() {
            _spriteRenderer.enabled = true;
        }
    }
}