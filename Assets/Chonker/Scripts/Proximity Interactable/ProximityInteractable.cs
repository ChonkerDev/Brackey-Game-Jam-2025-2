using System;
using UnityEngine;

namespace Chonker.Scripts.Proximity_Interactable
{
    public abstract class ProximityInteractable : MonoBehaviour
    {
        private void Awake() {
            _audioSource = GetComponentInChildren<AudioSource>();
        }

        protected AudioSource _audioSource;
        public abstract void OnProximityEnter(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector);
        public abstract void OnProximityExit(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector);
        public abstract void OnInteracted(PlayerRaccoonComponentContainer PlayerRaccoonComponentContainer);

        public virtual bool CanBeInteractedWith() {
            return true;
        }
    }
}