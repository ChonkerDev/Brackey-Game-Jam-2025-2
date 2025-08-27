using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionMenu : MonoBehaviour
{
    [SerializeField] private Button LeftButton;
    [SerializeField] private Button RightButton;


    private void Start() {
        
        gameObject.SetActive(false);
    }
}
