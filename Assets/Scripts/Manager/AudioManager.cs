using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
	TitleBGM = 0,
	GameBGM= 1,
	JumpSE= 2,
	TogeSE= 3,
	ExplosionSE= 4,
}

public class AudioManager : MonoBehaviour
{

	[SerializeField] AudioClip[] audioClips;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

	public void PlayBGM(AudioType audio)
	{
		audioSource.clip = audioClips[(int)audio];
		audioSource.Play();
	}

	public void StopBGM()
	{
		audioSource.clip = null;
		audioSource.Stop();
	}
}
