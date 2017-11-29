using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachDash : MonoBehaviour {

	JumpInstruction jumpInstruction;
	DashInstruction dashInstruction;

	// Use this for initialization
	void Start () {
		jumpInstruction = GetComponentInChildren<JumpInstruction>();
		dashInstruction = GetComponentInChildren<DashInstruction>();
	}

	// Update is called once per frame
	void Update () {

	}
}
