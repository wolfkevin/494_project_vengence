using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadyController : MonoBehaviour {

	bool[] readyCount = {false, false, false, false};

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		foreach (bool ready in readyCount) {
			Debug.Log(ready);
			if (!ready) return;
		}

		SceneManager.LoadScene("main_scene");
	}

	public void SetReady(int playerNum) {
		readyCount[playerNum] = true;
	}
}
