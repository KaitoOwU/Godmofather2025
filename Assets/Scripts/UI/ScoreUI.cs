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
        
        BG.DOAnchorPos3DY(77.324f, 0.3f).SetEase(Ease.OutBounce);
        Congrats.DOAnchorPos3DY(188.3603f, 0.6f).SetEase(Ease.OutBounce);
        Bravo.DOAnchorPos3DY(169.05f, 0.9f).SetEase(Ease.OutBounce);
        Timer.DOAnchorPos3DY(77.324f, 0.57f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            ((RectTransform)Quit.transform).DOAnchorPos3DY(-138, 0.57f).SetEase(Ease.OutBounce);
        });
    }
    
}
