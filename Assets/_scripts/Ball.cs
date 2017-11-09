using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private float maxSpeed = 20;

    private Rigidbody rb;
	private ParticleSystem ps;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
		ps = GameObject.FindGameObjectWithTag ("explosion").GetComponent<ParticleSystem>();
		ps.Stop ();
	}
	
	// Update is called once per frame
	void Update () {
        if (rb.velocity.magnitude > maxSpeed) {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground")) {
			Explode ();
            GameManager.instance.BallDown(gameObject);
        } 
    }

	void Explode(){
		ps.transform.position = this.gameObject.transform.position;
		ps.Play ();
	}
}
