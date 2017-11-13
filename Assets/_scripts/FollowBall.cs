using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour {

	public Transform ball;
	public float eyeRadius = 0.01f;
	Vector3 pupilPosition;

    private bool pauseFollowBall = false;

	// Use this for initialization
	void Start () {
		pupilPosition = this.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if (pauseFollowBall) {
            return;
        }
		Vector3 lookDirection = (ball.position - this.transform.position).normalized;
		this.transform.localPosition = pupilPosition + (lookDirection * eyeRadius);
	}

    public void PauseFollowBall() {
        pauseFollowBall = true;
    }

    public void ResumeFollowBall() {
        pauseFollowBall = false;
    }
}
