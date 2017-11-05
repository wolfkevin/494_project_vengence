using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class playerMovement : MonoBehaviour {

	public int playerNum = 0;

	private static float movementSpeed = 10;
	private static float jumpSpeed = 25;
    private static float dashSpeed = 35;
    private static float dashTime = .1f;

    private static float leapFrogVertThreshold = .2f;

	private Rigidbody rb;
	private InputDevice inputDevice;

    private bool jumped = false;
    private bool dashing = false;
    private bool dashed = false;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		inputDevice = InputManager.Devices[playerNum];


	}

    private void OnCollisionEnter(Collision collision)
    {
        if (jumped
            && !collision.gameObject.CompareTag("ball")
            && !collision.gameObject.CompareTag("wall")
            && !collision.gameObject.CompareTag("playerTeamA")
            && !collision.gameObject.CompareTag("playerTeamB")) {
            dashed = true;
        }

        if (collision.gameObject.CompareTag("ground")
            || collision.gameObject.CompareTag("netTop")) {
            jumped = false;
            dashed = false;
        }
        else if (collision.gameObject.CompareTag(gameObject.tag)
                 && transform.position.y - collision.gameObject.transform.position.y > leapFrogVertThreshold) {
            // Collided with teammate, and on top of them
            jumped = false;
            dashed = false;
        }
        else if (collision.gameObject.CompareTag("ball") && dashing)
        {
            Camera.main.GetComponent<CameraShake>().shakeDuration = .1f;
        }
    }

    // Update is called once per frame
    void Update()
	{
		if (inputDevice == null) {
			return;
		}

        //if (transform.position.y < 1.51)
        //{
        //    dashed = false;
        //}

		var xMovement = inputDevice.LeftStickX;
        var yMovement = inputDevice.LeftStickY;
        var vertical = rb.velocity.y;

        if (!dashing) {
            rb.velocity = new Vector3(xMovement * movementSpeed, vertical, 0);   
        }

		var jump = inputDevice.Action1.WasPressed;

        if (jump && !jumped) {
            jumped = true;
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

    public void ResetPlayer() {
        jumped = false;
        dashing = false;
        dashed = false;
    }
}
