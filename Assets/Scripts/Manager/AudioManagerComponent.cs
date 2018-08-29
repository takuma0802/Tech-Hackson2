using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
	BGM = 0,
	JumpSE= 1,
	DamageSE = 2,
	ExplosionSE= 3,
}

public class AudioManagerComponent : MonoBehaviour
{
    [SerializeField] private AudioSource bgmAudio;
	[SerializeField] private AudioSource soundEffectAudio;
	[SerializeField] private AudioClip[] audioClips;

    void Start()
    {
        bgmAudio.loop = true;
    }

	public void PlayBGM(AudioType audio)
	{
		bgmAudio.clip = audioClips[(int)audio];
		bgmAudio.Play();
	}

	public void StopBGM()
	{
		bgmAudio.clip = null;
		bgmAudio.Stop();
	}

	public void PlaySE(AudioType audio)
	{
		soundEffectAudio.clip = audioClips[(int)audio];
		soundEffectAudio.Play();
	}
}
