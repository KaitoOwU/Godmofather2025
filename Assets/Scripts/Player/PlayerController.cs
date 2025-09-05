using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private InputActionReference _interactInput;

    [Header("Movement Settings")]
    [SerializeField] private float _acceleration = 10f;
    [SerializeField] private float _deceleration = 15f;
    [SerializeField] private float _maxSpeed = 5f;

    [Header("Components")]
    [SerializeField] private GroundDetection _groundDetection;

    public bool Grounded => _groundDetection.Grounded;
    public STATE State => _state;

    private STATE _state;


    // physics
    private Rigidbody2D _rb;
    private Vector2 _velocity;
    private Vector2 _dir;

    // interaction
    private bool _canInteract;
    private IInteractable _iInteractable;
    private bool _hasControls = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        Init();
    }

    #region Inputs
    private void OnEnable()
    {
        _moveInput.action.started += OnMove;
        _moveInput.action.performed += OnMove;
        _moveInput.action.canceled += OnMoveCanceled;
        

        _interactInput.action.started += OnInteract;
        _interactInput.action.performed += OnInteract;
        
        _moveInput.action.Enable();
        _interactInput.action.Enable();
    }

    private void OnDisable()
    {
        _moveInput.action.started -= OnMove;
        _moveInput.action.performed -= OnMove;
        _moveInput.action.canceled -= OnMoveCanceled;

        _interactInput.action.started -= OnInteract;
        _interactInput.action.performed -= OnInteract;

        _moveInput.action.Disable();
        _interactInput.action.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        _dir = ctx.ReadValue<Vector2>().normalized;
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        _dir = Vector2.zero;
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        if (_canInteract) Interact();
        
    }

    #endregion

    private void Init()
    {
        _state = STATE.WALK;
    }

   
    private void FixedUpdate()
    {
        if(!_hasControls) return;

        Vector2 targetVelocity = Vector2.zero;

        switch (_state)
        {
            case STATE.WALK:
                if (_dir != Vector2.zero)
                {
                    //
                    if (!Grounded && _dir.y > 0)
                    {
                        _velocity = Vector2.zero;

                        _rb.linearVelocity = _velocity;
                        return;
                    }
                    else
                    {
                        targetVelocity = _dir * _maxSpeed;
                    }

                    // acceleration
                    _velocity = Vector2.MoveTowards(_velocity, targetVelocity, _acceleration * Time.fixedDeltaTime);
                }
                else
                {
                    // deceleration
                    _velocity = Vector2.MoveTowards(_velocity, Vector2.zero, _deceleration * Time.fixedDeltaTime);
                }
                break;

            case STATE.CLIMB:
                // acceleration
                if (_dir != Vector2.zero)
                {
                    if(Grounded)
                        targetVelocity = _dir * _maxSpeed; 
                    else
                        targetVelocity = new Vector2(0f, _dir.y * _maxSpeed);

                    _velocity = Vector2.MoveTowards(_velocity, targetVelocity, _acceleration * Time.fixedDeltaTime);
                }
                // deceleration
                else
                {
                    _velocity = Vector2.MoveTowards(_velocity, Vector2.zero, _deceleration * Time.fixedDeltaTime);
                }
                break;

            default:
                _velocity = Vector2.zero;
                break;
        }

        _rb.linearVelocity = _velocity;
    }

    #region Move
    public void Climb(Vector3 position)
    {
        // -- DEBUG --
        //Debug.Log("climb");

        _hasControls = false;

        _velocity = Vector2.zero;
        _rb.linearVelocity = Vector2.zero;
        transform.position = position;


        _state = STATE.CLIMB;

        _hasControls = true;

    }

    public void Walk()
    {
        // -- DEBUG --
        //Debug.Log("walk");

        _state = STATE.WALK;
    }

    public void SetControls(bool hasControls)
    {
        _hasControls = hasControls;

        if (_hasControls == false)
            _rb.linearVelocity = Vector2.zero;
    }

    #endregion

    #region Interfaces

    //Interaction
    private void Interact()
    {
        _iInteractable.Interact();
    }

    public void ResetInteractablity()
    {
        _iInteractable = null;
    }

    //Trigger

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<ITrigger>(out var enter))
        {
            enter.OnEnter();
        }

        //Interact
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            _canInteract = true;
            _iInteractable = interactable;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Interact
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            _canInteract = true;
            _iInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<ITrigger>(out var exit))
        {
            exit.OnExit();
        }

        //Interact
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            _canInteract = false;
            _iInteractable = null;
        }
    }


    #endregion
}

public enum STATE
{
    NONE = 0,
    WALK = 1,
    CLIMB = 2
}
