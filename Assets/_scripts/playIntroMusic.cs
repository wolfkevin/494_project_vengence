using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playIntroMusic : MonoBehaviour {

	AudioSource introSound;
	// AudioSource mainSound;
	// Scene scene;
	// int count = 0;
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(this.gameObject);
	}

	// Update is called once per frame
	void Start () {
		if (SceneManager.GetActiveScene().name == "main_scene"){
			introSound.Stop();
		} else if (!introSound.isPlaying){
			introSound.Play();
		}
	}
}
