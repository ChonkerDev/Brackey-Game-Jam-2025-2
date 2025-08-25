using UnityEngine;

public class RaccoonTrashcan : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _closedSprite;
    [SerializeField] private SpriteRenderer _openSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _openSprite.enabled = false;
    }

    public void EnterTrashcan() {
        _closedSprite.enabled = true;
        _openSprite.enabled = false;
    }

    public void ExitTrashcan() {
        _closedSprite.enabled = false;
        _openSprite.enabled = true;
    }
}
