using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public abstract class NavigationUIMenu : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    [SerializeField] protected Selectable defaultSelectable;
    [SerializeField] protected Selectable defaultSelectableOnDeactivate;
    private Coroutine coroutine;
    private RectTransform rectTransform;
    public RectTransform RectTransform => rectTransform;

    private static NavigationUIMenu currentFocusedMenu {
        get {
            if (navigationMenuStack.Count == 0) return null;
            return navigationMenuStack[^1];
        }
    }

    private static List<NavigationUIMenu> navigationMenuStack = new();

    [Obsolete("Use OnAwake instead", true)]
    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        OnAwake();
    }

    protected virtual void OnAwake() {
    }

    [Obsolete("Use OnUpdate instead", true)]
    private void Update() {
        if (currentFocusedMenu == this) {
            processCurrentMenu();
        }

        OnUpdate();
    }

    protected virtual void processCurrentMenu() {
    }

    protected virtual void OnUpdate() {
    }

    public virtual void Activate() {
        if (defaultSelectable) {
            EventSystem.current?.SetSelectedGameObject(defaultSelectable.gameObject);
        }

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        
        navigationMenuStack.Add(this);
    }

    public virtual void Deactivate() {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        if (defaultSelectableOnDeactivate) {
            EventSystem.current?.SetSelectedGameObject(defaultSelectableOnDeactivate.gameObject);
        }

        if (currentFocusedMenu == this) {
            navigationMenuStack.RemoveAt(navigationMenuStack.Count - 1);
        }
    }

    public void ClearCurrentInteractable() {
        EventSystem.current?.SetSelectedGameObject(null);
    }

    public void SetCanvasGroupAlpha(float alpha) {
        canvasGroup.alpha = alpha;
    }

    private void cleanMenuStack() {
        List<NavigationUIMenu> newStack = new();
        foreach (var navigationUIMenu in navigationMenuStack) {
            if (navigationUIMenu != null) {
                newStack.Add(navigationUIMenu);
            }
        }
        navigationMenuStack = newStack;
    }
}