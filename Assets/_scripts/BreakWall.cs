using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour {

	private float force;
	private ReadyController rc;
	private Transform grandfather;
	private AudioSource glassBreakingSound;


	void Start(){
		grandfather = this.transform.parent.parent.parent;
		rc = GameObject.FindGameObjectWithTag ("#ReadyController").GetComponent<ReadyController>();
		glassBreakingSound = GetComponentInParent<AudioSource>();
	}

	void OnCollisionEnter(Collision collision){
		Rigidbody rb = collision.gameObject.GetComponent<Rigidbody> ();
		force = collision.relativeVelocity.magnitude * rb.mass;
		if (force > 40f){
			glassBreakingSound.Play();
			Destroy(this.transform.parent.gameObject);
			grandfather.GetComponentInChildren<Checkbox>().SetCheckboxImage (true);
			rc.SetReady (grandfather.GetComponent<PlayerSelection> ().GetPlayerNum ());
		}
	}
}
