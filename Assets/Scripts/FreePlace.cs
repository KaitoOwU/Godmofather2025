using System.Diagnostics.Tracing;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public class FreePlace : MonoBehaviour
{
    [Header("Hover")]
    [SerializeField] private float _scaleIncrease = 1.2f;
    [SerializeField] private Color _interactableColor = Color.red;

    [Header("Events")]
    [SerializeField] private UnityEvent _OnClic;

    //Hover
    private Vector3 _scaleInit;
    private bool _isHovered;

    private bool _canInteract;
    private Color _defaultColor;

    //Components
    private SpriteRenderer _spriteRenderer;
    [SerializeField] BookBalanceManager _bookMgr;

    private void Awake()
    {
        _scaleInit = transform.localScale;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;
    }

    private void Start()
    {
        
    }

    private void OnMouseEnter()
    {
        if (!_canInteract) return;

        if (!_isHovered)
        {
            _isHovered = true;
            transform.localScale = _scaleInit * _scaleIncrease;
        }
    }

    private void OnMouseExit()
    {
        if (!_canInteract) return;

        if (_isHovered)
        {
            _isHovered = false;
            transform.localScale = _scaleInit;
        }
    }

    private void OnMouseDown()
    {
        if (!_canInteract) return;

        _OnClic?.Invoke();
        //BookBalanceManager.OnBookPlacementStarted?.Invoke(Vector3.zero);
    }

    public void SetVisibility(bool visible)
    {
        _canInteract = visible;

        // highlight free places
        if (_canInteract)
            _spriteRenderer.color = _interactableColor;
        else
            _spriteRenderer.color = _defaultColor; 

    }
}
