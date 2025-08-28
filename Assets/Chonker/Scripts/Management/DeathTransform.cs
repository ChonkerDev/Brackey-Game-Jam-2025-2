using UnityEngine;

namespace Chonker.Scripts.Management
{
    public struct DeathTransform
    {
        public DeathTransform(Vector3 position, float rotation) {
            Position = position;
            Rotation = rotation;
        }
        public Vector2 Position;
        public float Rotation;
    }
}