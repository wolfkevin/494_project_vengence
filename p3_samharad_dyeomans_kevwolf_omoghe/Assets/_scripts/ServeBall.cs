using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeBall : MonoBehaviour {
	public GameObject springPrefab;
	private SpringJoint springJoint;

	private Rigidbody jointA;
	private Rigidbody jointB;

	private Vector2 ballHomePositionA = new Vector2(-11.5f, 8f);
	private Vector2 ballHomePositionB;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		ballHomePositionB = ballHomePositionA;
		ballHomePositionB.x *= -1;
		jointA = GameObject.FindGameObjectWithTag ("jointA").GetComponent<Rigidbody>();
		jointB = GameObject.FindGameObjectWithTag ("jointB").GetComponent<Rigidbody>();
		rb = GetComponent<Rigidbody>();

		ResetBall(Random.value < .5 ? GameManager.Teams.TeamA : GameManager.Teams.TeamB);
	}


	void weakenBreakForce(){
		if (springJoint) {
			springJoint.breakForce -= 1.5f;
		}
	}


	void OnCollisionEnter(){
		if (springJoint) {
			springJoint.breakForce = .1f;
		}
	}
		

	public void ResetBall(GameManager.Teams teamThatScored) {
		rb.velocity = Vector3.zero;
		rb.rotation = Quaternion.identity;

		springJoint = gameObject.AddComponent<SpringJoint>();
		springJoint.breakForce = 50;
		springJoint.breakTorque = 10;
		InvokeRepeating ("weakenBreakForce", 2.0f, 2.0f);

		if (teamThatScored == GameManager.Teams.TeamA) {
			this.transform.position = ballHomePositionA;
			springJoint.connectedBody = jointA;
		} else {
			this.transform.position = ballHomePositionB;
			springJoint.connectedBody = jointB;
		}
	}
}
