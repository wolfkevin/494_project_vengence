using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class DashIndicator : MonoBehaviour {

	const float bodyOffsetForDirectionalArrow = 1f;

	PlayerMovement pm;
	GameObject directionalArrow;

	private InputDevice inputDevice;
	private float xInput;
	private float yInput;
	Vector3 dashScale;

	// Use this for initialization
	void Start () {
		pm = this.gameObject.GetComponentInParent<PlayerMovement> ();
		directionalArrow = this.transform.GetChild (0).gameObject;
		directionalArrow.SetActive(false);

		inputDevice = GetComponentInParent<PlayerInputDevice>().inputDevice;
		dashScale = this.transform.localScale;
	}

	// Update is called once per frame
	void Update () {
        inputDevice = GetComponentInParent<PlayerInputDevice>().inputDevice;
		if (pm.IsCharging ()) {
			directionalArrow.SetActive (true);
			xInput = inputDevice.LeftStickX;
			yInput = inputDevice.LeftStickY;
			directionalArrow.transform.localRotation = Quaternion.AngleAxis(Mathf.Atan2(yInput, xInput) * Mathf.Rad2Deg - 90, Vector3.forward);

			directionalArrow.transform.localPosition = new Vector3 (xInput * bodyOffsetForDirectionalArrow, yInput * bodyOffsetForDirectionalArrow, 0);
		} else {
			ResetDash();
		}
	}

	public void ResetDash() {
			this.transform.localScale = dashScale;
			directionalArrow.SetActive (false);
	}

	public void GrowDash() {
		this.transform.localScale *= 1.01f;
	}
}
