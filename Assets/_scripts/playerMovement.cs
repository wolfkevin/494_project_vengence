using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class playerMovement : MonoBehaviour {

	public int playerNum = 0;

	public float movementSpeed;
	public float jumpSpeed;

	private Rigidbody rb;
	private InputDevice inputDevice;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		inputDevice = InputManager.Devices[playerNum];
	}

	// Update is called once per frame
	void Update()
	{
		if (inputDevice == null) {
			return;
		}

		var horizontal = inputDevice.LeftStickX;
		rb.velocity = Vector2.right * horizontal * movementSpeed;

		var jump = inputDevice.Action1.WasPressed;
		if (jump) {
			rb.AddForce(Vector3.up * jumpSpeed);
		}
			

	}


}
