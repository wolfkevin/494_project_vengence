using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class playerMovement : MonoBehaviour {

	public int playerNum = 0;

	private static float movementSpeed = 10;
	private static float jumpSpeed = 30;

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

		var xMovement = inputDevice.LeftStickX;
        var yMovement = inputDevice.LeftStickY;
        var vertical = rb.velocity.y;
        rb.velocity = new Vector3(xMovement * movementSpeed, vertical, yMovement * movementSpeed);
		//rb.velocity = Vector2.right * horizontal * movementSpeed;

		var jump = inputDevice.Action1.WasPressed;
        if (jump && transform.position.y < 1.51) {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
		}
			

	}


}
