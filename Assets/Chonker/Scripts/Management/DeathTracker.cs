using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chonker.Scripts.Management
{
    public class DeathTracker : MonoBehaviour
    {
        public static DeathTracker instance;

        private SceneManagerWrapper.SceneId LastLoadedScene;
        public List<DeathTransform> DeathTransforms = new List<DeathTransform>();

        private void Awake() {
            if (!instance) {
                instance = this;
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
        }


        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            SceneManagerWrapper.SceneId newScene = SceneManagerWrapper.GetSceneId(scene);
            if (newScene != LastLoadedScene) {
                DeathTransforms.Clear();
            }

            LastLoadedScene = newScene;
        }
    }
}