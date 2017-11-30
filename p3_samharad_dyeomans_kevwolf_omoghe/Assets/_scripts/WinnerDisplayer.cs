using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerDisplayer : MonoBehaviour {
	static string rightWins = "RIGHT TEAM WINS";
	static string leftWins = "LEFT TEAM WINS";

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
			if (gm.GetLeftTeamScore() < gm.GetRightTeamScore()) {
				title.text = rightWins;
			} else {
				title.text = leftWins;
			}
		}
	}
}
