using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    private static AudioManagerComponent _audioManagerComponent;

    private static AudioManagerComponent Manager
    {
        get
        {
            if (_audioManagerComponent != null) return _audioManagerComponent;

            _audioManagerComponent = GameManager.Instance.AudioManager;
            return _audioManagerComponent;
        }
    }

    public static void PlayBGM(AudioType bgm)
    {
        Manager.PlayBGM(bgm);
    }

    public static void StopBGM()
    {
        Manager.StopBGM();
    }

    public static void PlaySoundEffect(AudioType soundEffect)
    {
        Manager.PlaySE(soundEffect);
    }
}
