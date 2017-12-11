using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void Play() {
		Debug.Log("play game");
        Initiate.Fade("sandbox_scene", Color.black, 1f);
		//SceneManager.LoadScene("selection_scene");
	}

	public void Exit() {
		Debug.Log("exit game");
		Application.Quit();
	}

	public void Options() {
		Debug.Log("options menu");
		// TODO
	}
}
