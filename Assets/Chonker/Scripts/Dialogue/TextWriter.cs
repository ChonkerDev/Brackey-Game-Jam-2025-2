using System.Collections;
using TMPro;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    private bool skipProcessing;
    public bool TextProcessingFinished { get; private set; }
    public void FillText(float timePerCharacter, string text) {
        StartCoroutine(processText(timePerCharacter, text));
    }

    private IEnumerator processText(float timePerCharacter, string text) {
        int currentIndex = 0;
        int stringLength = text.Length;
        TextProcessingFinished = false;
        while (currentIndex < stringLength) {
            if (skipProcessing) {
                break;
            }
            currentIndex++;
            yield return new WaitForSeconds(timePerCharacter);
            _textMeshProUGUI.text = text.Substring(0, currentIndex);
        }

        TextProcessingFinished = true;
        skipProcessing = false;
        _textMeshProUGUI.text = text;
    }

    public void SkipProcessing() {
        skipProcessing = true;
    }
}
