using System;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class BookBalancing : MonoBehaviour
{
    [field:SerializeField] public Image BalanceGauge { get; private set; }
    [field:SerializeField] public Image BalanceGoodSpot { get; private set; }
    [field:SerializeField] public Image BalanceCursor { get; private set; }
    [field:SerializeField] public Transform BookTransform { get; private set; }
    [field:SerializeField] public Color MinColor { get; private set; }
    [field:SerializeField] public Color MaxColor { get; private set; }
    [field:SerializeField] public float TimeBeforeLoss { get; private set; }

    private float _balanceInterval;
    private float _balanceCursorInterval;
    private float _completionState;
    
    private float GaugeBalanceWideness => (BalanceGauge.rectTransform.rect.width - BalanceGoodSpot.rectTransform.rect.width) / 2.0f;
    private float GaugeWideness => BalanceGauge.rectTransform.rect.width / 2.0f;

    private void Awake()
    {
        BalanceGoodSpot.color = MinColor;
    }

    private void Update()
    {
        TimeBeforeLoss -= Time.deltaTime;
        if (TimeBeforeLoss <= 0f)
        {
            GameManager.Instance.BookBalance.BookPlacementFailed();
            Destroy(gameObject);
        }
        
        _balanceInterval = Mathf.PingPong(Time.time, 2.0f) - 1.0f;
        BalanceGoodSpot.rectTransform.position = new Vector3(_balanceInterval * GaugeBalanceWideness, BalanceGoodSpot.rectTransform.position.y,
            BalanceGoodSpot.rectTransform.position.z);

        _balanceCursorInterval =
            Mathf.Clamp(_balanceCursorInterval + Input.GetAxis("Horizontal") * (GameManager.Instance.Malus.AreControlsReversed ? -1 : 1) * Time.deltaTime * 2.0f, -1f, 1f);
        BalanceCursor.rectTransform.position = new Vector3(_balanceCursorInterval * GaugeWideness, BalanceCursor.rectTransform.position.y,
            BalanceCursor.rectTransform.position.z);

        if (IsGaugeInsideGoodZone())
        {
            _completionState += Time.deltaTime / 3.0f;
            BalanceGoodSpot.color = Color.Lerp(MinColor, MaxColor, _completionState);

            if (_completionState >= 1.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private bool IsGaugeInsideGoodZone()
    {
        return Mathf.Abs(BalanceGoodSpot.transform.position.x - BalanceCursor.transform.position.x) <
               BalanceGoodSpot.rectTransform.rect.width / 2.0f + BalanceCursor.rectTransform.rect.width / 2.0f;
    }
}
