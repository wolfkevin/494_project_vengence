using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerSelection : MonoBehaviour {
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
	}

	GameObject FindCheckbox() {
		foreach (Transform child in transform) {
			if (child.gameObject.tag == "checkbox") {
				return child.gameObject;
			}
		}

		return null;
	}

	public int GetPlayerNum(){
		return playerNum;
	}

}
