using System.Diagnostics.Tracing;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public class Place : MonoBehaviour
{
    [Header("Hover")]
    [SerializeField] private float _scaleIncrease = 1.2f;
    [SerializeField] private Color _interactableColor = Color.green;

    [Header("Events")]
    [SerializeField] private UnityEvent<Vector3> _OnClic;


    public bool IsFree => _isFree;
    private bool _isFree;

    //interact
    private bool _canInteract;
    private Color _freePlaceColor;
    private Color _defaultColor = Color.white;

    //Hover
    private Vector3 _scaleInit;
    private bool _isHovered;

    //Components
    private SpriteRenderer _spriteRenderer;

    //Manager
    BookBalanceManager _bookMgr;
    InventoryManager _inventoryMgr;

    private bool CanPlaceBook => _canInteract && _inventoryMgr.IsFull && _isFree;

    private void Awake()
    {
        _scaleInit = transform.localScale;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _freePlaceColor = _spriteRenderer.color;
    }

    private void Start()
    {
        _bookMgr = FindAnyObjectByType<BookBalanceManager>();
        _inventoryMgr = FindAnyObjectByType<InventoryManager>();

        if (_isFree) SetFreePlaceBehaviour(true);
    }

    // -- INTERACT --
    private void OnMouseEnter()
    {
        if (!CanPlaceBook) return;

        if (!_isHovered)
        {
            _isHovered = true;
            transform.localScale = _scaleInit * _scaleIncrease;
        }
    }

    private void OnMouseExit()
    {
        if (!CanPlaceBook) return;

        if (_isHovered)
        {
            _isHovered = false;
            transform.localScale = _scaleInit;
        }
    }

    private void OnMouseDown()
    {
        if (!CanPlaceBook) return;

        _OnClic?.Invoke(transform.position);
        _inventoryMgr.SetMyPlace(this.gameObject);
    }

    // -- FREE PLACE --
    //Called in LibraryManager
    public void BecameFreePlace()
    {
        //Debug.Log("jsuis la");
        _isFree = true;
    }

    public void FillFreePlace()
    {
        SetFreePlaceBehaviour(false);
    }

    private void SetFreePlaceBehaviour(bool isFree)
    {
        if (isFree)
        {
            _freePlaceColor = Color.gray;
            _spriteRenderer.color = _freePlaceColor;
        }
        else
        {
            _spriteRenderer.color = _defaultColor;
            _isFree = false;
        }
    }

    // -- AREAS ---
    // Called in LibraryArea
    public void SetVisibility(bool visible)
    {
        if (_spriteRenderer == null) return;

        _canInteract = visible;

        // highlight free places
        if (CanPlaceBook)
            _spriteRenderer.color = _interactableColor;
        else if (_isFree)
            _spriteRenderer.color = _freePlaceColor;
        else
            _spriteRenderer.color = _defaultColor;

    }
}
