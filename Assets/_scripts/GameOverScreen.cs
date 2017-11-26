using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	}

	public void Restart() {
		Debug.Log("restart game");
        Initiate.Fade("main_scene", Color.black, 1f);
		//SceneManager.LoadScene("main_scene");
	}

	public void Exit() {
		Debug.Log("exit game");
		Application.Quit();
	}

	public void MainMenu() {
		Debug.Log("main menu");
        Initiate.Fade("title_scene", Color.black, 1f);
		//SceneManager.LoadScene("title_scene");
	}
}
