using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour {

	public float force;
	private ReadyController rc;
	private Transform grandfather;


	void Start(){
		grandfather = this.transform.parent.parent.parent;
		rc = GameObject.FindGameObjectWithTag ("#ReadyController").GetComponent<ReadyController>();
	}

	void OnCollisionEnter(Collision collision){
		Rigidbody rb = collision.gameObject.GetComponent<Rigidbody> ();
//		if (collision.rigidbody)
//			otherMass = collision.rigidbody.mass;
//		else 
//			otherMass = 1000; // static collider means huge mass'
//		collision.r
		force = collision.relativeVelocity.magnitude * rb.mass;
		if (force > 40f){
			Destroy(this.transform.parent.gameObject);
			grandfather.GetComponentInChildren<Checkbox> ().SetCheckboxImage (true);
			rc.SetReady (grandfather.GetComponent<PlayerSelection> ().GetPlayerNum ());
		}
	}
}
