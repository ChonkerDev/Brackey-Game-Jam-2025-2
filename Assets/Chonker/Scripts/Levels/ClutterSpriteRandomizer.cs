using System;
using UnityEngine;

namespace Chonker.Scripts.Levels
{
    public class ClutterSpriteRandomizer : MonoBehaviour
    {
        [SerializeField] private Sprite[] possibleSprites;

        private void Start() {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = possibleSprites[UnityEngine.Random.Range(0, possibleSprites.Length)];
        }
    }
}