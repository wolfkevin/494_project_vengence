using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedBall : MonoBehaviour {
	GameObject lastHitBy;
	Rigidbody rb;

	// multiplier for dash hit velocity
	float DEFAULT_CHARGE = 1f;
	float CHARGE_BONUS = .1f;
	float charge;

	// ball scale
	static Vector3 baseScale;

	// Use this for initialization
	void Start () {
		ResetCharge();
		rb = GetComponent<Rigidbody>();
		baseScale = transform.localScale;
	}

	// Update is called once per frame
	void Update () {
		// Debug.Log(charge);
		transform.localScale = baseScale * charge;
	}

	void OnCollisionEnter(Collision collision) {
		GameObject other = collision.gameObject;

		if (other.CompareTag("playerTeamA") || other.CompareTag("playerTeamB")) {
			// someone hit the ball
			if (lastHitBy == null) {
				// first hit (serve)
				lastHitBy = other;
				charge += CHARGE_BONUS;
			} else {
				// grab PlayerMovement script
				PlayerMovement pm = (PlayerMovement) other.GetComponent<PlayerMovement>();

				if (pm.IsDashing()) {
					// dashes consume charge regardless of team
					ApplyCharge();
					ResetCharge();
				} else {
					// only do something if a different player hits the ball
					if (other != lastHitBy) {
						if (lastHitBy.CompareTag(other.tag)) {
							// same team, add charge
							charge += CHARGE_BONUS;
						} else {
							// different team, reset charge
							ResetCharge();
						}
					}
				}
			}
		}

		if (other.CompareTag("ground")) {
			ResetCharge();
		}
	}

	void ResetCharge() {
		Debug.Log("resetting charge");
		// reset charge
		charge = DEFAULT_CHARGE;
		// reset lastHitBy
		lastHitBy = null;
	}

	void ApplyCharge() {
		Debug.Log("applying charge: " + charge.ToString());
		// apply charge to velocity
		if (rb != null) {
			rb.velocity  = rb.velocity * charge;
		}
	}
}
