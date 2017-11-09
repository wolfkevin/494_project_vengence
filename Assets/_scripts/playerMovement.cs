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

    private Transform dashIndicator;

    private Vector2 lastDirection = Vector2.right;

    // Jump variables
    private float gravity;
    private bool jumped = false;
    private bool jumping = false;
    private bool dashed = false;
    private bool dashing = false;
    private bool charging = false;

    private float minJumpHeight = 1f;
    private float maxJumpHeight = 6f;

    private float minJumpVelocity;
    private float maxJumpVelocity;

    private float timeToJumpApex = .4f;

    private float dashChargeFactor = 1f;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		inputDevice = InputManager.Devices[playerNum];

        dashIndicator = transform.Find("DashIndicator");

        // Jump variable runtime initialization
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
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
            || (collision.gameObject.CompareTag("netTop") && transform.position.y - collision.gameObject.transform.position.y > .9)) {
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
        if (inputDevice == null)
        {
            return;
        }

        // Get controller input
        var xInput = inputDevice.LeftStickX;
        var yInput = inputDevice.LeftStickY;
        var actionButtonIsPressed = inputDevice.Action1.IsPressed;
        var actionButtonWasPressed = inputDevice.Action1.WasPressed;
        var actionButtonWasReleased = inputDevice.Action1.WasReleased;

        // For storing new velocity values
        var newXVelocity = rb.velocity.x;
        var newYVelocity = rb.velocity.y;

        // Simple movement, on the ground or in the air
        if (!charging && !dashing) {
            newXVelocity = xInput * movementSpeed;
        }

        var gravityToApply = gravity;

        // Initiate jump
        if (actionButtonWasPressed && !jumped) {
            Debug.Log("simple jump");
            jumping = true;
            newYVelocity = maxJumpVelocity;
        } 
        // Jump early abort (small jump)
        else if (actionButtonWasReleased && jumping) {
            Debug.Log("end simple jump");
            jumping = false;
            jumped = true;
            if (rb.velocity.y > minJumpVelocity) {
                newYVelocity = minJumpVelocity;
            }
        }
        // Initiate dash charge
        else if (actionButtonWasPressed && jumped && !dashed) {
            Debug.Log("initiate dash charge");
            charging = true;
            newXVelocity = 0;
            newYVelocity = 0;
            gravityToApply = 0f;
        }
        // Continue charging dash
        else if (actionButtonIsPressed && jumped && !dashed) {
            Debug.Log("charging dash");
            newXVelocity = 0;
            newYVelocity = 0;
            dashChargeFactor += .02f;
            gravityToApply = 0f;
        }
        // Activate dash
        else if (actionButtonWasReleased && jumped && !dashed) {
            Debug.Log("release dash");
            charging = false;
            var dashDirection = new Vector2(xInput, yInput);
            StartCoroutine(Dash(dashDirection));
        }

        // Dashing is taken care of by coroutine
        if (!dashing) {
            // Apply gravity
            newYVelocity += gravityToApply * Time.deltaTime;

            // Update velocity
            rb.velocity = new Vector3(newXVelocity, newYVelocity, 0);
        }
	}

    IEnumerator Dash(Vector2 direction) {
        dashed = true;
        dashing = true;
        //rb.velocity = direction * dashSpeed * dashChargeFactor;
        rb.velocity = direction * dashSpeed;
        yield return new WaitForSeconds(dashTime);
        rb.velocity = Vector2.zero;
        dashChargeFactor = 1f;
        dashing = false;
    }

    public void ResetPlayer() {
        jumped = false;
        dashing = false;
        dashed = false;
    }
}
