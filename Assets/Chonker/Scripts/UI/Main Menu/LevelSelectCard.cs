using System;
using Chonker.Core.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chonker.Scripts.Management
{
    public class LevelSelectCard : MonoBehaviour
    {
        [SerializeField, PrefabModeOnly] private TextMeshProUGUI _titleText;
        [SerializeField, PrefabModeOnly] private Image levelImage;
        [SerializeField, PrefabModeOnly] private TextMeshProUGUI _timeText;
        public SceneManagerWrapper.SceneId LevelId;
        public void SetData(LevelSelectCardData data) {
            LevelId = data.SceneId;
            _titleText.text = data.GetLevelName();
            levelImage.overrideSprite = data.sprite;
            float time = PersistantDataManager.instance.GetLevelTime(data.SceneId);
            string formattedTime;
            if (time == float.MaxValue) {
                formattedTime = "--:--:--";
            }
            else {
                TimeSpan t = TimeSpan.FromSeconds(time);
                formattedTime = string.Format("{0:D2}:{1:D2}.{2:D3}",
                    t.Minutes,
                    t.Seconds,
                    t.Milliseconds);  
            }

            _timeText.text = "Your Time\n" + formattedTime;
        }
    }
}