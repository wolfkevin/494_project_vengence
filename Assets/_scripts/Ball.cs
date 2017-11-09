using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private float maxSpeed = 20;

    private Rigidbody rb;

    private float gravity = -9.71f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

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
            GameManager.instance.BallDown(gameObject);
        } 
    }
}
