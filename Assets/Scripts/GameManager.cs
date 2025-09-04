using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public BookBalanceManager BookBalance { get; private set; }
    public MalusManager Malus { get; private set; }

    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject);

        Instance = this;

        BookBalance = FindAnyObjectByType<BookBalanceManager>();
        Malus = FindAnyObjectByType<MalusManager>();
    }
}
