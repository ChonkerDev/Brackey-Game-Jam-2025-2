using UnityEngine;

namespace Chonker.Scripts.Proximity_Interactable
{
    public abstract class ProximityInteractable : MonoBehaviour
    {
        
        public abstract void OnProximityEnter(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector);
        public abstract void OnProximityExit(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector);
        public abstract void OnInteracted(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector);
    }
}