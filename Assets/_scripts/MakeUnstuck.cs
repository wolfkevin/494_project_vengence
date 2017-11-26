using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeUnstuck : MonoBehaviour {

    private Rigidbody rb;

    private float unstickThreshold = 0.1f;
    private float unstickVelocity = 10;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay(Collision collision)
    {
        var velocity = rb.velocity;
         
        if (Mathf.Abs(velocity.x) < unstickThreshold) {
            velocity.x += unstickVelocity;
        }
        if (Mathf.Abs(velocity.y) < unstickThreshold) {
            velocity.y += unstickVelocity;   
        }

        rb.velocity = velocity;
    }


}
