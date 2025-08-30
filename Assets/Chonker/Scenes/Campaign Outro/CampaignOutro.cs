using Chonker.Core;
using UnityEngine;

public class CampaignOutro : MonoBehaviour
{
    [SerializeField] private DialogueProcessor DialogueProcessor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake() {
        Time.timeScale = 1f;
        DialogueProcessor.OnFinishDialogue.AddListener(() => {
            ScreenFader.FadeOut(2, () => { SceneManagerWrapper.LoadScene(SceneManagerWrapper.SceneId.MainMenu); },
                EaseType.EaseOutQuad);
        });
    }
}