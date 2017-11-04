using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class playerSelectionMovement : MonoBehaviour {

	Rigidbody rb;

	public int playerNum = 0;
	public float movementSpeed;
	private InputDevice inputDevice;
	private GameObject checkbox;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
		inputDevice = InputManager.Devices[playerNum];
		checkbox = GameObject.FindGameObjectWithTag ("checkbox");
		checkbox.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		var horizontal = inputDevice.LeftStickX;
		var vertical = inputDevice.LeftStickY;
		rb.velocity = new Vector3(1f * horizontal * movementSpeed, 1f * vertical * movementSpeed);

		var ready = inputDevice.Action1.WasPressed;
		if (ready) {
			checkbox.SetActive (true);
		}
	}

}
