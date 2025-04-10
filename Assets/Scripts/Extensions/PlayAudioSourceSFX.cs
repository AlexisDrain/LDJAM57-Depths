﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioSourceSFX: MonoBehaviour {

	public float overrideAudioSourceVolume = 1f;
	//private Transform myTransform;

	public void PlaySFX () {

		GetComponent<AudioSource>().PlayWebGL(GetComponent<AudioSource>().clip, overrideAudioSourceVolume);
		//myTransform = transform;
	}
	public void PlayLoudSFXInGameManager(AudioClip newAudioClip) {

		GameManager.SpawnLoudAudio(newAudioClip, new Vector2(), overrideAudioSourceVolume);
    }
    public void PlayVariablePitchSFXInGameManager(AudioClip newAudioClip) {

        GameManager.SpawnLoudAudio(newAudioClip, new Vector2(0.8f, 1.2f), overrideAudioSourceVolume);
    }


    /*
	private void Update () {
		
	}
	*/
}
