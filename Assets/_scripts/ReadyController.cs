using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadyController : MonoBehaviour {

	bool[] readyCount = { false, false, false, false};

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		foreach (bool ready in readyCount) {
			if (!ready) return;
		}
      StartCoroutine(LoadMainScene());
	}

    IEnumerator LoadMainScene() {
        yield return new WaitForSeconds(.3f);
        Initiate.Fade("main_scene", Color.black, 1f);
    }

	public void SetReady(int playerNum) {
		readyCount[playerNum] = true;
	}
}
