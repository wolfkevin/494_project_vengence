using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeBall : MonoBehaviour {
	public GameObject springPrefab;
	private SpringJoint springJoint;

	private Rigidbody jointA;
	private Rigidbody jointB;

	private Vector2 ballHomePositionA = new Vector2(-11.5f, 6f);
	private Vector2 ballHomePositionB = new Vector2(11.5f, 6f);
	private Rigidbody rb;


	// Use this for initialization
	void Start () {

		jointA = GameObject.FindGameObjectWithTag ("jointA").GetComponent<Rigidbody>();
		jointB = GameObject.FindGameObjectWithTag ("jointB").GetComponent<Rigidbody>();
		rb = GetComponent<Rigidbody>();

		ResetBall(Random.value < .5 ? GameManager.Teams.TeamA : GameManager.Teams.TeamB);
	}

	public void ResetBall(GameManager.Teams teamThatScored) {
//		GameObject tempSpring = Instantiate (springPrefab, this.transform);
//		springJoint = tempSpring.GetComponent<SpringJoint> ();
		springJoint = gameObject.AddComponent<SpringJoint>();
		springJoint.breakForce = 25;
		springJoint.breakTorque = 25;

		if (teamThatScored == GameManager.Teams.TeamA) {
			this.transform.position = ballHomePositionA;
			springJoint.connectedBody = jointA;
		} else {
			this.transform.position = ballHomePositionB;
			springJoint.connectedBody = jointB;
		}

        GetComponent<Rigidbody>().velocity = Vector2.zero;
	}
}
