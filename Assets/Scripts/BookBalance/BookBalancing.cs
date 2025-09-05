using System;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;

public class BookBalancing : MonoBehaviour
{
    [field:SerializeField] public Image BalanceGauge { get; private set; }
    [field:SerializeField] public Image BalanceGoodSpot { get; private set; }
    [field:SerializeField] public Image BalanceCursor { get; private set; }
    [field:SerializeField] public Color MinColor { get; private set; }
    [field:SerializeField] public Color MaxColor { get; private set; }
    [field:SerializeField] public float TimeBeforeLoss { get; private set; }

    private float _balanceInterval;
    private float _balanceCursorInterval;
    private float _completionState;
    
    private float GaugeBalanceWideness => (BalanceGauge.rectTransform.rect.width - BalanceGoodSpot.rectTransform.rect.width) / 2.0f;
    private float GaugeWideness => BalanceGauge.rectTransform.rect.width / 2.0f;

    //Scripts
    InventoryManager _inventory;

    //book infos
    private GameObject _myPlace;

    private void Awake()
    {
        BalanceCursor.color = MinColor;
    }

    private void Start()
    {
        _inventory= FindAnyObjectByType<InventoryManager>();
    }

    private void Update()
    {
        _balanceCursorInterval = Mathf.Clamp01(_balanceCursorInterval + Time.deltaTime * Input.GetAxis("Horizontal") * (GameManager.Instance.Malus.AreControlsReversed ? -1 : 1));
        _balanceInterval = Mathf.PingPong(Time.time / 1.5f, 1.0f);

        BalanceGoodSpot.rectTransform.localPosition = new Vector3(Mathf.Lerp(-1.425f, 1.425f, _balanceInterval), BalanceGoodSpot.rectTransform.localPosition.y, BalanceGoodSpot.rectTransform.localPosition.z);
        BalanceCursor.rectTransform.localPosition = new Vector3(Mathf.Lerp(-1.7512f, 1.7512f, _balanceCursorInterval), BalanceCursor.rectTransform.localPosition.y, BalanceCursor.rectTransform.localPosition.z);

        if (IsGaugeInsideGoodZone())
        {
            _completionState += Time.deltaTime / 3.0f;
            BalanceCursor.color = Color.Lerp(MinColor, MaxColor, _completionState);

            if (_completionState >= 1.0f)
            {
                // call win
                _inventory.PlaceBook();

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
