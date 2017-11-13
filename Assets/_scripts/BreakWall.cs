using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour {

	public float force;

	void OnCollisionEnter(Collision collision){
		Rigidbody rb = collision.gameObject.GetComponent<Rigidbody> ();
//		if (collision.rigidbody)
//			otherMass = collision.rigidbody.mass;
//		else 
//			otherMass = 1000; // static collider means huge mass'
//		collision.r
		force = collision.relativeVelocity.magnitude * rb.mass;
		if (force > 55f){
			this.transform.parent.parent.parent.GetComponentInChildren<Checkbox> ().SetCheckboxImage (true);
			Destroy(this.transform.parent.gameObject);
		}
	}
}
