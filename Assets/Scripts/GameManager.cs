using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [field:SerializeField] public ScoreUI Score { get; private set; }
    [field:SerializeField] public SoundManager Sounds { get; private set; }
    public BookBalanceManager BookBalance { get; private set; }
    public InventoryManager Inventory { get; private set; }
    public MalusManager Malus { get; private set; }

    private float _timer;

    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject);

        Instance = this;

        BookBalance = FindAnyObjectByType<BookBalanceManager>();
        Malus = FindAnyObjectByType<MalusManager>();
        Inventory = FindAnyObjectByType<InventoryManager>();

        Inventory.OnWin += Win;
    }

    private void Win()
    {
        Score.ShowEndgameUI(_timer);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Score.ShowEndgameUI(_timer);
        }
    }

    public void BackToMainMenu() => SceneManager.LoadScene("MainMenu");
}
