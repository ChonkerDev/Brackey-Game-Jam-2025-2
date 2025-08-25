using System;
using UnityEngine;

namespace Chonker.Scripts.Player_Raccoon
{
    public class PlayerRaccoonView : MonoBehaviour
    {
        private PlayerRaccoonComponentContainer playerRaccoonComponentContainer;
        [SerializeField] private Animator _animator;
        [SerializeField] SpriteRenderer _spriteRenderer;

        private int animatorIsRunningHash; 

        private void Awake() {
            playerRaccoonComponentContainer = GetComponentInParent<PlayerRaccoonComponentContainer>();
            animatorIsRunningHash = Animator.StringToHash("IsRunning");
        }

        private void Update() {
            _animator.SetBool(animatorIsRunningHash, playerRaccoonComponentContainer.PlayerRaccoonController.Velocity.sqrMagnitude > .01f);
        }

        public void HideModel() {
            _spriteRenderer.enabled = false;
        }

        public void ShowModel() {
            _spriteRenderer.enabled = true;
        }
    }
}