using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueProcessor : MonoBehaviour
{
    [SerializeField] private TextWriter TextWriter;
    [SerializeField] private DialogueSet DialogueSet;
    public UnityEvent OnFinishDialogue;

    private void Awake() {
        ScreenFader.TurnOff();
    }

    IEnumerator Start() {
        int currentDialogueIndex = 0;
        while (currentDialogueIndex < DialogueSet.dialogues.Length) {
            string currentDialogueText = DialogueSet.dialogues[currentDialogueIndex];
            TextWriter.FillText(.05f, currentDialogueText);
            while (!TextWriter.TextProcessingFinished) {
                if (PlayerInputWrapper.instance.wasDialogueProceedPressedThisFrame()) {
                    TextWriter.SkipProcessing();
                }
                yield return null;
            }
            while (!PlayerInputWrapper.instance.wasDialogueProceedPressedThisFrame()) {
                yield return null;
            }

            yield return null;// eat a frame so input doesn't carry over
            currentDialogueIndex++;
        }
        OnFinishDialogue.Invoke();
    }
}