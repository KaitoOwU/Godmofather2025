using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [field:SerializeField] public ScoreUI Score { get; private set; }
    [field:SerializeField] public SoundManager Sounds { get; private set; }
    public BookBalanceManager BookBalance { get; private set; }
    public MalusManager Malus { get; private set; }

    private float _timer;

    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject);

        Instance = this;

        BookBalance = FindAnyObjectByType<BookBalanceManager>();
        Malus = FindAnyObjectByType<MalusManager>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Score.ShowEndgameUI(_timer);
        }
    }
}
