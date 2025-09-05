using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    
    [field:SerializeField] public TMP_Text TimerText { get; private set; }
    [field: SerializeField] public RectTransform Congrats { get; private set; }
    [field: SerializeField] public RectTransform Bravo { get; private set; }
    [field: SerializeField] public RectTransform Timer { get; private set; }
    [field: SerializeField] public RectTransform BG { get; private set; }
    [field: SerializeField] public Button Quit { get; private set; }

    public void ShowEndgameUI(float timer)
    {
        TimerText.text = TimeSpan.FromSeconds(timer).Minutes + ":" + TimeSpan.FromSeconds(timer).Seconds + ":" + TimeSpan.FromSeconds(timer).Milliseconds;
        
        BG.DOAnchorPos3DY(0f, 0.3f).SetEase(Ease.OutBounce);
    }
    
}
