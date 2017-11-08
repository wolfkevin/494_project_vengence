using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour {

	// Update is called once per frame
	public void resetScene () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
