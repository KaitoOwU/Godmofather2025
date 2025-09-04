using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class BookBalanceManager : MonoBehaviour
{

    [field:SerializeField] public GameObject BookObject { get; private set; }
    
    public event Action<Vector3> OnBookPlacementStarted;
    public event Action OnBookPlacementFailed;
    public event Action OnBookPlacementCompleted;
    

    private void Update()
    {
    }

    public void BookPlacementStarted(Vector3 position)
    {
        OnBookPlacementStarted?.Invoke(position);
    }

    public void BookPlacementCompleted()
    {
        OnBookPlacementCompleted?.Invoke();
    }

    private void OnEnable()
    {
        OnBookPlacementStarted += StartBookPlacement;
    }

    private void OnDisable()
    {
        OnBookPlacementStarted -= StartBookPlacement;
    }

    private void StartBookPlacement(Vector3 pos) => Instantiate(BookObject, pos, Quaternion.identity);

    public void BookPlacementFailed() => OnBookPlacementFailed?.Invoke();
}
