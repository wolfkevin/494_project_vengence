using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class ResetScene : MonoBehaviour {

	InputDevice inputDevice;

	void Start(){
		inputDevice = GetComponent<PlayerInputDevice>().inputDevice;

	}

	// Update is called once per frame
	void Update () {
		if ((inputDevice != null) && inputDevice.MenuWasPressed){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
