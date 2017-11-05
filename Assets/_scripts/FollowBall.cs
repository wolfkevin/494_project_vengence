using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour {

	public Transform ball;
	public float eyeRadius = 0.01f;
	Vector3 pupilPosition;

	// Use this for initialization
	void Start () {
		pupilPosition = this.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 lookDirection = (ball.position - this.transform.position).normalized;
		this.transform.localPosition = pupilPosition + (lookDirection * eyeRadius);
	}
}
