using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class playerMovement : MonoBehaviour {

	public int playerNum = 0;

	private static float movementSpeed = 10;
	private static float jumpSpeed = 15;
    private static float dashSpeed = 35;
    private static float dashTime = .1f;

	private Rigidbody rb;
	private InputDevice inputDevice;

    private bool dashing = false;
    private bool dashed = false;

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

        if (transform.position.y < 1.51)
        {
            dashed = false;
        }

		var xMovement = inputDevice.LeftStickX;
        var yMovement = inputDevice.LeftStickY;
        var vertical = rb.velocity.y;

        if (!dashing) {
            rb.velocity = new Vector3(xMovement * movementSpeed, vertical, 0);   
        }

		var jump = inputDevice.Action1.WasPressed;

        if (jump && transform.position.y < 1.51) {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        } else if (jump && !dashed) {
            Vector2 direction = new Vector2(xMovement, yMovement);
            StartCoroutine(Dash(direction));
        }
			

	}

    IEnumerator Dash(Vector2 direction) {
        dashed = true;
        dashing = true;
        rb.velocity = direction * dashSpeed;
        yield return new WaitForSeconds(dashTime);
        rb.velocity = Vector2.zero;
        dashing = false;
    }


}
