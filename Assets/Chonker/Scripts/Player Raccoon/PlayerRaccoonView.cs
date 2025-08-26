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
        private int isDeadBoolHash;

        private float altIdleTimer;
        private float altIdleTime = 10;

        private PlayerStateManager playerStateManager => playerRaccoonComponentContainer.PlayerStateManager;

        private void Awake() {
            playerRaccoonComponentContainer = GetComponentInParent<PlayerRaccoonComponentContainer>();
            animatorIsRunningHash = Animator.StringToHash("IsRunning");
            altIdleTriggerHash = Animator.StringToHash("Alt Idle");
            isDeadBoolHash = Animator.StringToHash("Is Dead");
            GetComponentInChildren<CinemachineCamera>().transform.parent = null;
            GetComponentInChildren<Camera>().transform.parent = null;
        }

        private void Start() {
            playerStateManager.OnStateChange.AddListener(((old, newState) => {
                if (old == newState) return;
                switch (newState) {
                    case PlayerStateId.Movement:
                        _animator.SetBool(isDeadBoolHash, false);
                        break;
                    case PlayerStateId.Hidden:
                        _animator.SetBool(isDeadBoolHash, false);
                        _animator.SetBool(animatorIsRunningHash, false);
                        break;
                    case PlayerStateId.Dead:
                        _animator.SetBool(isDeadBoolHash, true);
                        _animator.SetBool(animatorIsRunningHash, false);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }));
        }

        private void Update() {
            switch (playerRaccoonComponentContainer.PlayerStateManager.CurrentState) {
                case PlayerStateId.Movement:
                    bool isMoving = playerRaccoonComponentContainer.PlayerRaccoonController.Velocity.sqrMagnitude >
                                    .01f;
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
                    break;
                case PlayerStateId.Hidden:

                    break;
                case PlayerStateId.Dead:

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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