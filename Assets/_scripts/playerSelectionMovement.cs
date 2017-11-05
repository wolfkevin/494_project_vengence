using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerSelectionMovement : MonoBehaviour {
	public int playerNum = 0;

	private InputDevice inputDevice;
	private GameObject checkbox;
	private GameObject readyController;

	// Use this for initialization
	void Start () {
		inputDevice = InputManager.Devices[playerNum];

		checkbox = FindCheckbox();
		if (checkbox != null) checkbox.GetComponent<SpriteRenderer>().enabled = false;

		readyController = GameObject.FindWithTag("#ReadyController");
	}

	// Update is called once per frame
	void Update () {
		if (inputDevice == null) return;

		// Y button to ready up
		if (inputDevice.Action4.WasPressed) {
			Debug.Log("Y pressed");

			if (checkbox != null) checkbox.GetComponent<SpriteRenderer>().enabled = true;
			else Debug.Log("checkbox is null");

			if (readyController != null) {
				ReadyController rc = (ReadyController) readyController.GetComponent<ReadyController>();
				rc.SetReady(playerNum);
			}
		}
	}

	GameObject FindCheckbox() {
		foreach (Transform child in transform) {
			if (child.gameObject.tag == "checkbox") {
				return child.gameObject;
			}
		}

		return null;
	}

}
