using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public abstract class NavigationUIMenu : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    [SerializeField] protected Selectable defaultSelectable;
    [SerializeField] protected Selectable defaultSelectableOnDeactivate;
    
    [Obsolete("Use OnAwake instead", true)]
    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        OnAwake();
    }

    protected virtual void OnAwake() {
        
    }

    public virtual void Activate() {
        if (defaultSelectable) {
            EventSystem.current?.SetSelectedGameObject(defaultSelectable.gameObject);
        }
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public virtual void Deactivate() {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        if (defaultSelectableOnDeactivate) {
            EventSystem.current?.SetSelectedGameObject(defaultSelectableOnDeactivate.gameObject);
        }
    }

    public void ClearCurrentInteractable() {
        EventSystem.current?.SetSelectedGameObject(null);
    }
    
}
