using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedBall : MonoBehaviour {
	GameObject lastHitBy;
	Rigidbody rb;

	// multiplier for dash hit velocity
	int charge = 1;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision) {
		GameObject other = collision.gameObject;

		if (other.CompareTag("playerTeamA") || other.CompareTag("playerTeamB")) {
			// someone hit the ball
			if (lastHitBy == null) {
				// first hit (serve)
				lastHitBy = other;
				charge++;
			} else {
				// grab playerMovement script
				playerMovement pm = (playerMovement) other.GetComponent<playerMovement>();

				if (pm.IsDashing()) {
					// dashes consume charge regardless of team
					ApplyCharge();
					ResetCharge();
				} else {
					// only do something if a different player hits the ball
					if (other != lastHitBy) {
						if (lastHitBy.CompareTag(other.tag)) {
							// same team, add charge
							charge++;
						} else {
							// different team, reset charge
							ResetCharge();
						}
					}
				}
			}
		}
	}

	void ResetCharge() {
		Debug.Log("resetting charge");
		// reset charge
		charge = 1;
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
