using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PupilDashIndicator : MonoBehaviour {

	private Vector3 pupilScale;
	private Vector3 pupilHomePosition;
	private FollowBall followBall;

	// Use this for initialization
	void Start () {

		pupilScale = this.transform.localScale;
		pupilHomePosition = this.transform.localPosition;
		followBall = this.GetComponent<FollowBall>();

	}

	// Update is called once per frame
	void Update () {

	}



	public void ResetPupil() {
			this.transform.localScale = pupilScale;
			followBall.ResumeFollowBall();
	}

	public void GrowPupil() {
		transform.localPosition = pupilHomePosition;
			this.transform.localScale *= 1.01f;
	}

}
