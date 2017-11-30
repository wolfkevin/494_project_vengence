using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionDisplayer : MonoBehaviour {
    public string FIRST_TO = "FIRST TO 7";
	public string WIN_BY_TWO = "WIN BY 2";
	public string GAME_POINT = "GAME POINT";
	static string EMPTY = "";

	Text text;
	bool displaying = false;

	// Use this for initialization
    void Start () {
        //FIRST_TO = "FIRST TO " + GameManager.instance.scoreToWin;
		text = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
	}

	public void DisplayMessage(string message) {
        //UpdateText();
		StartCoroutine(DisplayMessageCoroutine(message));
	}

	IEnumerator DisplayMessageCoroutine(string message) {
		if (text == null) text = GetComponent<Text>();

		displaying = true;
		text.text = message;
		yield return new WaitForSeconds(4f);
		text.text = EMPTY;
		displaying = false;
	}
}
