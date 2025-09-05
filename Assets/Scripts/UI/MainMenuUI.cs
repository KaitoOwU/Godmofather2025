using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{

    [field: SerializeField] public RectTransform _mainMenu;
    [field: SerializeField] public Button _play;
    [field: SerializeField] public Button _quit;

    private void OnEnable()
    {
        _play.onClick.AddListener(() => SceneManager.LoadScene("dev-Ambre"));
        _quit.onClick.AddListener(Application.Quit);
    }

    private void OnDisable()
    {
        _play.onClick.RemoveAllListeners();
        _quit.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        _mainMenu.DOAnchorPos3DY(-135, 1f).SetEase(Ease.OutBounce);
        ((RectTransform)_play.transform).DOAnchorPos3DY(-293, 1.5f).SetEase(Ease.OutBounce);
        ((RectTransform)_quit.transform).DOAnchorPos3DY(-423, 2f).SetEase(Ease.OutBounce);
    }
}
