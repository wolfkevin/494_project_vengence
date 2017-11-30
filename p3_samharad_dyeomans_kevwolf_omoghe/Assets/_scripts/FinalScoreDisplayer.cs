using UnityEngine;
using UnityEngine.UI;

public class FinalScoreDisplayer : MonoBehaviour {
	Text title;

	// Use this for initialization
	void Start () {
		title = GetComponent<Text>();
		title.text = GameData.LeftSideScore + " - " + GameData.RightSideScore;
	}

	// Update is called once per frame
	void Update () {
	}
}
