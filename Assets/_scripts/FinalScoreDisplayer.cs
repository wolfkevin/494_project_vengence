using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreDisplayer : MonoBehaviour {
	Text title;

	// Use this for initialization
	void Start () {
		title = (Text) GetComponent<Text>();
		title.text = GameData.LeftSideScore.ToString() + " - " + GameData.RightSideScore.ToString();
	}

	// Update is called once per frame
	void Update () {
	}
}
