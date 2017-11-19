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
			directionalArrow.transform.localRotation = Quaternion.AngleAxis(Mathf.Atan2(yInput, xInput) * Mathf.Rad2Deg - 90, Vector3.forward);
			directionalArrow.transform.localPosition = new Vector3 (xInput * bodyOffsetForDirectionalArrow, yInput * bodyOffsetForDirectionalArrow, 0);


		
			print (Mathf.Tan (yInput / xInput) * Mathf.Rad2Deg * 2);
		} else {
			directionalArrow.SetActive (false);
		}
	}
}
