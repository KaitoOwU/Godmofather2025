using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class MalusManager : MonoBehaviour
{
    
    [field:SerializeField] public float MalusDuration { get; private set; }
    [field:SerializeField] public Transform MalusesUI { get; private set; }
    
    public float SpeedMultiplier { get; private set; }
    public bool AreControlsReversed { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            StartCoroutine(ApplyMalus(MalusType.RANDOM_DVD_SPRITE));
        }
    }

    public IEnumerator ApplyMalus(MalusType type)
    {
        switch (type)
        {
            case MalusType.SPEED_REDUCED:
                
                yield return DOTween.To(() => SpeedMultiplier, (x) => SpeedMultiplier = x, 0.5f, 0.5f).WaitForCompletion();
                yield return new WaitForSecondsRealtime(MalusDuration);
                yield return DOTween.To(() => SpeedMultiplier, (x) => SpeedMultiplier = x, 0.5f, 0.5f).WaitForCompletion();
                
                break;
            case MalusType.SCREEN_REVERSED:

                yield return Camera.main.transform.DORotate(new Vector3(0, 0, 180), 1f).SetEase(Ease.InExpo).WaitForCompletion();
                yield return new WaitForSecondsRealtime(MalusDuration);
                yield return Camera.main.transform.DORotate(new Vector3(0, 0, 0), 1f).SetEase(Ease.InExpo).WaitForCompletion();
                
                break;
            case MalusType.KEYBINDS_REVERSED:

                AreControlsReversed = true;
                yield return new WaitForSecondsRealtime(MalusDuration);
                AreControlsReversed = false;
                
                break;
            case MalusType.RANDOM_DVD_SPRITE:

                Image[] spriteList = Resources.LoadAll<Image>("Sprites");
                Image spriteToAdd = spriteList[UnityEngine.Random.Range(0, spriteList.Length)];

                Image img = Instantiate(spriteToAdd, MalusesUI.transform);

                yield return img.DOColor(new(1, 1, 1, 0.5f), 0.5f).WaitForCompletion();

                float duration = 0;
                while (duration < MalusDuration)
                {
                    
                    
                    duration += Time.deltaTime;
                    yield return null;
                }
                
                yield return img.DOColor(new(1, 1, 1, 0f), 0.5f).WaitForCompletion();
                
                Destroy(img);
                break;
        }
    }
    
}

public enum MalusType
{
    SPEED_REDUCED,
    SCREEN_REVERSED,
    KEYBINDS_REVERSED,
    RANDOM_DVD_SPRITE
}
