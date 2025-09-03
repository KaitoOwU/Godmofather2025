using System;
using System.Collections;
using UnityEngine;

public class BookBalanceManager : MonoBehaviour
{

    [field:SerializeField] public GameObject BookObject { get; private set; }
    
    public event Action<Vector3> OnBookPlacementStarted;
    public event Action OnBookPlacementCompleted;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            OnBookPlacementStarted?.Invoke(Vector3.zero);
        }
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
}
