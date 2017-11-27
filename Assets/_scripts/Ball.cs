using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private float maxSpeed = 50;
    public AudioSource clinkSound;
    private Rigidbody rb;
	private ParticleSystem ps;

    private float gravity = -12f;

	// Use this for initialization
	void Start () {
    clinkSound = GetComponent<AudioSource>();
    rb = GetComponent<Rigidbody>();
		ps = GameObject.FindGameObjectWithTag ("explosion").GetComponent<ParticleSystem>();
		ps.Stop ();
	}

	// Update is called once per frame
	void Update () {
        // Apply gravity to ball
        var newYVelocity = rb.velocity.y;
        newYVelocity += gravity * Time.deltaTime;

        // Update velocity for gravity
        rb.velocity = new Vector2(rb.velocity.x, newYVelocity);

        // Cap ball speed
         if (rb.velocity.magnitude > maxSpeed) {
             rb.velocity = rb.velocity.normalized * maxSpeed;
         }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground")) {
			      Explode ();
            GameManager.instance.BallDown(gameObject);
        } else {
          clinkSound.Play();
        }
    }

	void Explode(){
		ps.transform.position = this.gameObject.transform.position;
		ps.Play ();
		ps.GetComponent<ExplosionKnockback> ().ExplodeAndKnockBack ();
	}
}
