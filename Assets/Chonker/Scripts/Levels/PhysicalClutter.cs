using System;
using System.Linq;
using UnityEngine;

namespace Chonker.Scripts.Levels
{
    public class PhysicalClutter : MonoBehaviour
    {
        [SerializeField] private Sprite[] possibleSprites;
        [SerializeField] private AudioSource audioSource;
        private int[] validLayers;
        private void Start() {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = possibleSprites[UnityEngine.Random.Range(0, possibleSprites.Length)];
            validLayers = new[] {
                LayerMask.NameToLayer("Clutter"),
                LayerMask.NameToLayer("Player"),
                LayerMask.NameToLayer("AI")
            };
        }
        
        private void OnCollisionEnter2D(Collision2D other) {
            int layer = other.gameObject.layer;
            if (validLayers.Contains(layer)) {
                audioSource.Play();
            }
        }
    }
}