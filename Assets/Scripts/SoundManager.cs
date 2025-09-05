using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioClip _takeBookSound, _putBookSound, _moveLadderSound, _curseAppearSound, _malusSound, _spriteOnScreenMalusSound, _balanceSound;
    private AudioSource _audioSource;

    public enum AudioType
    {
        TAKE_BOOK,
        PUT_BOOK,
        MOVE_LADDER,
        CURSE_APPEAR,
        MALUS,
        SPRITE_ON_SCREEN,
        BALANCE
    };

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioType type)
    {
        switch (type)
        {
            case AudioType.TAKE_BOOK:
                _audioSource.PlayOneShot(_takeBookSound);
                break;
            case AudioType.PUT_BOOK:
                _audioSource.PlayOneShot(_putBookSound);
                break;
            case AudioType.MOVE_LADDER:
                _audioSource.PlayOneShot(_moveLadderSound);
                break;
            case AudioType.CURSE_APPEAR:
                _audioSource.PlayOneShot(_curseAppearSound);
                break;
            case AudioType.MALUS:
                _audioSource.PlayOneShot(_malusSound);
                break;
            case AudioType.SPRITE_ON_SCREEN:
                _audioSource.PlayOneShot(_spriteOnScreenMalusSound);
                break;
            case AudioType.BALANCE:
                _audioSource.PlayOneShot(_balanceSound);
                break;
        }
    }
}
