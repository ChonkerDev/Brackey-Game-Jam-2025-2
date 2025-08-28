using System;
using Chonker.Scripts.Management;
using UnityEngine;

namespace Chonker.Scripts.Levels
{
    public class DeathSpritePlacer : MonoBehaviour
    {
        [SerializeField] private Sprite RaccoonDeadSprite;

        private void Start() {
            foreach (var instanceDeathTransform in DeathTracker.instance.DeathTransforms) {
                GameObject go = new GameObject {
                    transform = {
                        position = instanceDeathTransform.Position,
                        eulerAngles = new Vector3(0, 0, instanceDeathTransform.Rotation)
                    }
                };
                go.name = "Dead Raccoon";
                SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                sr.sprite = RaccoonDeadSprite;
                sr.sortingOrder = 3;
            }
        }
    }
}