using System;
using UnityEngine;

namespace Chonker.Scripts.Management
{
    [Serializable]
    public struct LevelSelectCardData
    {
        public SceneManagerWrapper.SceneId SceneId;
        public Sprite sprite;

        public string GetLevelName() {
            string displayName = SceneId.ToString();
            displayName = displayName.Insert(5, " ");
            return displayName;
        }
        
    }
}