using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerMovement : MonoBehaviour
{
    private static float movementSpeed = 18;
    private static float jumpSpeed = 25;
    private static float dashSpeed = 35;
    private static float dashTime = .1f;
    private static float leapFrogVertThreshold = .2f;
	private static float higherJumpOnPartnerVelocity = 15;

    private Rigidbody rb;
    private InputDevice inputDevice;
    private Vector2 lastDirection = Vector2.right;

    // Jump variables
    // Credit to SebLague for the jumping code generally
    // https://github.com/SebLague/2DPlatformer-Tutorial/blob/master/Platformer%20E10/Assets/Scripts/Player.cs
    private float gravity;
    private bool grounded = false;
    private bool jumped = false;
    private bool jumping = false;
    private bool dashed = false;
    private bool dashing = false;
    private bool charging = false;
    private float minJumpHeight = .5f;
    private float maxJumpHeight = 6f;
    private float minJumpVelocity;
    private float maxJumpVelocity;
    private float timeToJumpApex = .4f;
    private float dashChargeFactor = 1f;
    private float maxDashChargeFactor = 2.5f;
    private Vector3 positionStartedCharging;

    private bool allowMotion = true;

    private ParticleSystem particleSystem;

    private float shakeSpeed = 1f;
    private float shakeDistance = .15f;

    private FollowBall followBall;
    private PupilDashIndicator pupilDashIndicator;

    private float lastXinput;
    private float lastYinput;
    AudioSource dashSound;

	private bool onPartner = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputDevice = this.gameObject.GetComponent<PlayerInputDevice>().inputDevice;

        // Jump variable runtime initialization
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

        particleSystem = this.GetComponentInChildren<ParticleSystem>();
        particleSystem.Stop();

        followBall = this.GetComponentInChildren<FollowBall>();
        pupilDashIndicator = this.GetComponentInChildren<PupilDashIndicator>();
        dashSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        // TODO: the problem is that we initialize inputDevice in start, as does PlayerInputDevice,
        // and we don't know which Start() runs first I don't think
        inputDevice = this.gameObject.GetComponent<PlayerInputDevice>().inputDevice;

        if (fix)
        {
            rb.transform.position = fixPosition;
        }

        if (!allowMotion) { return; }

        // For storing new velocity values
        var newXVelocity = rb.velocity.x;
        var newYVelocity = rb.velocity.y;
        var gravityToApply = gravity;

        if (inputDevice == null || !allowMotion)
        {
            // Apply gravity
            newYVelocity += gravityToApply * Time.deltaTime;

            // Update velocity
            rb.velocity = new Vector3(newXVelocity, newYVelocity, 0);
            return;
        }

        // Get controller input
        var xInput = inputDevice.LeftStickX;
        var yInput = inputDevice.LeftStickY;

        if ((Mathf.Abs(xInput) > 0.05f) && (Mathf.Abs(yInput) > 0.05f)){
          lastXinput = xInput;
          lastYinput = yInput;
        }

        var actionButtonIsPressed = inputDevice.Action1.IsPressed;
        var actionButtonWasPressed = inputDevice.Action1.WasPressed;
        var actionButtonWasReleased = inputDevice.Action1.WasReleased;

        if (!actionButtonIsPressed) {
            charging = false;
            pupilDashIndicator.ResetPupil();
        }

        // Simple movement, on the ground or in the air
        if (!charging && !dashing)
        {
            newXVelocity = xInput * movementSpeed;
        }

        // Initiate jump
        if (actionButtonWasPressed && !jumped)
        {
            jumping = true;
            newYVelocity = maxJumpVelocity;

			if (onPartner) {
				newYVelocity += higherJumpOnPartnerVelocity;
			}
        }
        // Jump early abort (small jump)
        else if (actionButtonWasReleased && jumping)
        {
            jumping = false;
            jumped = true;
            if (rb.velocity.y > minJumpVelocity)
            {
                newYVelocity = minJumpVelocity;
            }
        }
        // Initiate dash charge
        else if (actionButtonWasPressed && jumped && !dashed)
        {
            positionStartedCharging = transform.position;
            charging = true;
            newXVelocity = 0;
            newYVelocity = 0;
            gravityToApply = 0f;
        }
        // Continue charging dash
        else if (actionButtonIsPressed && jumped && !dashed)
        {
            Shake();
            followBall.PauseFollowBall();
            pupilDashIndicator.GrowPupil();
            newXVelocity = 0;
            newYVelocity = 0;
            dashChargeFactor += .012f;
            gravityToApply = 0f;
            if (dashChargeFactor > 2f) {
                charging = false;
                dashed = true;
                dashChargeFactor = 1f;
                pupilDashIndicator.ResetPupil();
            }
        }
        // Activate dash
        else if (actionButtonWasReleased && jumped && !dashed)
        {
            pupilDashIndicator.ResetPupil();
            charging = false;
            var dashDirection = ((xInput != 0f) && (yInput != 0f)) ? new Vector2(xInput, yInput) : new Vector2(lastXinput, lastYinput);
            StartCoroutine(Dash(dashDirection));
        }


        // Dashing is taken care of by coroutine
        if (!dashing)
        {
            // Apply gravity
            newYVelocity += gravityToApply * Time.deltaTime;

            // Update velocity
            rb.velocity = new Vector3(newXVelocity, newYVelocity, 0);
        }
    }

    private void FixedUpdate()
    {
        grounded = false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball") && dashing)
        {
            Camera.main.GetComponent<CameraShake>().shakeDuration = .1f;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Vector3.Dot(collision.contacts[0].normal, Vector3.up) > .65)
        {
            grounded = true;
			onPartner = false;
            jumped = false;
            dashed = false;
            lastXinput = 0f;
            lastYinput = 0f;
        }

  		if (isOnSameTeam(collision.gameObject) && this.transform.position.y > collision.gameObject.transform.position.y) {
  			onPartner = true;
  		}
    }

    private void Shake() {
        transform.position = (Vector2)positionStartedCharging + Random.insideUnitCircle * shakeDistance;
    }

    IEnumerator Dash(Vector2 direction)
    {
        dashSound.Play();
        dashed = true;
        dashing = true;
        var chargeFactor = Mathf.Min(dashChargeFactor, maxDashChargeFactor);
        rb.velocity = direction * dashSpeed * chargeFactor;
        particleSystem.Play();
        yield
        return new WaitForSeconds(dashTime);
        rb.velocity = Vector2.zero;
        dashChargeFactor = 1f;
        dashing = false;
        particleSystem.Stop();
    }

    public void DisallowMotion() {
        allowMotion = false;
        rb.mass = 999999;
    }

    public void AllowMotion() {
        allowMotion = true;
        rb.mass = 1;
    }

    private Vector2 fixPosition;
    private bool fix = false;
    public void FixPosition() {
        fixPosition = transform.position;
        fix = true;
    }

    public void UnfixPosition() {
        fix = false;
    }

    public void ResetPlayer()
    {
        jumped = false;
        jumping = false;
        dashed = false;
        dashing = false;
        charging = false;
        pupilDashIndicator.ResetPupil();
    }

    public bool IsDashing() {
      return dashing;
    }

	public bool IsCharging(){
		return charging;
	}

  	public bool IsGrounded(){
    	return grounded;
  	}

	bool isOnSameTeam(GameObject otherPlayer){
		if (otherPlayer.GetComponent<RedTeam> () && this.GetComponent<RedTeam> ()) {
			return true;
		} else if (otherPlayer.GetComponent<BlueTeam> () && this.GetComponent<BlueTeam> ()) {
			return true;
		} else {
			return false;
		}
	}
}
