using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControllerTwo : MonoBehaviour {

	public AudioClip startMusic;
	public AudioClip gameMusic;
	public AudioClip endMusic;

	private AudioSource myAudioSource;

	void Start()
	{
		myAudioSource = GetComponent<AudioSource>();
		myAudioSource.clip = startMusic;
		myAudioSource.Play();
	}
	
	void GameMusic()
	{
		//myAudioSource.clip = gameMusic;
		//myAudioSource.Play();
	}

	public void EndMusic()
	{
		myAudioSource.clip = endMusic;
		myAudioSource.Play();
	}
}