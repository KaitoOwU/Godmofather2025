using UnityEngine;
using UnityEngine.Events;

public class Triggerable : MonoBehaviour, IInteractable, ITrigger
{

    [Header("Events")]
    [SerializeField] private UnityEvent _OnEnter;
    [SerializeField] private UnityEvent _OnExit;
    [SerializeField] private UnityEvent _OnInteract;

    public void Interact()
    {
        _OnInteract?.Invoke();  
    }

    public void OnEnter()
    {
        _OnEnter?.Invoke();
    }

    public void OnExit()
    {
        _OnExit?.Invoke();
    }
}
