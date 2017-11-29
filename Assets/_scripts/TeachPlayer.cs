using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachPlayer : MonoBehaviour {

	TeachDash teachDash;
	// GameObject teachTransform;

	// Use this for initialization
	void Start () {
		teachDash = GetComponentInChildren<TeachDash>();
	}

	// Update is called once per frame
	void Update () {

	}

	public void DashTaught(){
		Destroy(teachDash.gameObject);
	}
}
