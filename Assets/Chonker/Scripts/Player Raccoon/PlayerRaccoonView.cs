using UnityEngine;

namespace Chonker.Scripts.Player_Raccoon
{
    public class PlayerRaccoonView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] SpriteRenderer _spriteRenderer;

        public void HideModel() {
            _spriteRenderer.enabled = false;
        }

        public void ShowModel() {
            _spriteRenderer.enabled = true;
        }
    }
}