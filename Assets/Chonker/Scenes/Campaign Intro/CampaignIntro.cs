using Chonker.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CampaignIntro : MonoBehaviour
{
    [SerializeField] private DialogueProcessor DialogueProcessor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        DialogueProcessor.OnFinishDialogue.AddListener(() => {
            ScreenFader.FadeOut(2, null, EaseType.EaseOutQuad);
            SceneManagerWrapper.LoadScene(SceneManagerWrapper.SceneId.Level1);
        });
    }
}
