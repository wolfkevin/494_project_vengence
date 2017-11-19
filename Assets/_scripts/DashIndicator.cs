using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class DashIndicator : MonoBehaviour {

	const float bodyOffsetForDirectionalArrow = 1.6f;

	PlayerMovement pm;
	GameObject directionalArrow;

	private InputDevice inputDevice;

	// Use this for initialization
	void Start () {
		pm = this.gameObject.GetComponentInParent<PlayerMovement> ();
		directionalArrow = GameObject.FindGameObjectWithTag ("directionalArrow");
		directionalArrow.SetActive(false);

		inputDevice = InputManager.Devices[pm.playerNum];
	}
	
	// Update is called once per frame
	void Update () {
		if (pm.IsCharging ()) {
			directionalArrow.SetActive (true);
			var xInput = inputDevice.LeftStickX;
			var yInput = inputDevice.LeftStickY;
			directionalArrow.transform.localPosition = new Vector3 (xInput * bodyOffsetForDirectionalArrow, yInput * bodyOffsetForDirectionalArrow, 0);
//			directionalArrow.transform.LookAt (this.gameObject.transform, Vector3.back);
		} else {
			directionalArrow.SetActive (false);
		}
	}
}
