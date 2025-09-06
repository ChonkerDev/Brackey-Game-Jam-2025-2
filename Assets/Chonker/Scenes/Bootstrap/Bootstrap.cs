using System.Collections;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    void Start() {
        SceneManagerWrapper.LoadScene(SceneManagerWrapper.SceneId.MainMenu);
    }
}