using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	Animator anim;
	Rigidbody rb;
	bool grounded = true;

	// Use this for initialization
	void Start () {
		anim = this.GetComponentInParent<Animator> ();
		rb = this.GetComponentInParent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 groundedVelocity = rb.velocity;
		groundedVelocity.y = 0;
		anim.SetFloat ("Speed", Mathf.Max(1, groundedVelocity.magnitude * 0.5f));
		anim.SetFloat ("VerticalSpeed", rb.velocity.y);
	}

	void OnCollisionEnter(Collision collision){
		grounded = true;
	}
}
