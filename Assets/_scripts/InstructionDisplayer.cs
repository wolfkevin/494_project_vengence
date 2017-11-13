using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionDisplayer : MonoBehaviour {
	public static string FIRST_TO_7 = "FIRST TO 7";
	public static string GAME_POINT = "GAME POINT";
	static string EMPTY = "";

	Text text;
	bool displaying = false;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
	}

	public void DisplayMessage(string message) {
		StartCoroutine(_DisplayMessage(message));
	}

	IEnumerator _DisplayMessage(string message) {
		displaying = true;
		text.text = message;
		yield return new WaitForSeconds(5f);
		text.text = EMPTY;
		displaying = false;
	}
}
