using UnityEngine;

using UnityEngine.Events;

public class Ladder : MonoBehaviour, IInteractable, ITrigger
{
    #region Variables
    [Header("Climb")]
    [SerializeField] private Transform _startingPoint;
    [SerializeField] private Collider2D _libraryColl;

    [Header("Ladder Coll")]
    [SerializeField] private Collider2D _ladderColl;
    [SerializeField] private Collider2D _interactColl;

    [Header("Events")]
    [SerializeField] private UnityEvent _OnTriggerEnter;
    [SerializeField] private UnityEvent _OnTriggerExit;
    [SerializeField] private UnityEvent _OnInteract;

    private Rigidbody2D _rb;

    private PlayerController _pc;
    private STATE _state => _pc.State;

    #endregion


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        _pc = _player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    #region Interfaces

    public void Interact()
    {
        //// -- DEBUG --
        //Debug.Log("on interact");

        //SetLadderBehaviour((int)STATE.CLIMB);

        //// -- EVENTS --
        //_OnInteract?.Invoke();
    }

    public void SetLadderBehaviour(int state)
    {
        // WALK
        if(state == (int)STATE.WALK)
        {
            // Update pc behaviour
            _pc.Walk();

            // -- DEBUG --
            //Debug.Log("walk ladder");

            // -- BEHAVIOUR --

            _ladderColl.enabled = true;
            _ladderColl.isTrigger = false;

            _interactColl.enabled = true;
            _interactColl.isTrigger = true;

            // -- TO EDIT --
            // library coll
            _libraryColl.enabled = true;

        }
        // CLIMB
        else if(state == (int)STATE.CLIMB) 
        {
            // Update pc behaviour
            _pc.Climb(_startingPoint.position);

            // -- DEBUG --
            //Debug.Log("climb ladder");

            // -- BEHAVIOUR --
            _rb.linearVelocity = Vector2.zero;

            _ladderColl.isTrigger = true;
            _interactColl.enabled = false;

            // -- TO EDIT --
            // library coll
            _libraryColl.enabled = false;
        }
    }

    public void OnEnter()
    {
        // -- EVENTS --
        _OnTriggerEnter?.Invoke();
    }

    public void OnExit()
    {
        //if (_pc.Grounded)
        //{
        //    _pc.Walk();

        //    // -- EVENTS --
        //    _OnTriggerExit?.Invoke();
        //}
    }

    #endregion
}
