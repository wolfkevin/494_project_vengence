using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScoreDisplayer : MonoBehaviour {
	Text title;
	GameManager gm;

	// Use this for initialization
	void Start () {
		title = (Text) GetComponent<Text>();

		GameObject gmo = GameObject.FindWithTag("gameManager");
		if (gmo != null) {
			gm = (GameManager) gmo.GetComponent<GameManager>();
		}
	}

	// Update is called once per frame
	void Update () {
		if (gm != null) {
			title.text = gm.GetLeftTeamScore().ToString() + " - " + gm.GetRightTeamScore().ToString();
		}
	}
}
