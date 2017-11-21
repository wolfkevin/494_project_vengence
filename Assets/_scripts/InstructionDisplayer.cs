using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionDisplayer : MonoBehaviour {
    public string FIRST_TO = "FIRST TO 7";
	public string GAME_POINT = "GAME POINT";
	static string EMPTY = "";

	Text text;
	bool displaying = false;

    private void Awake()
    {
        //FIRST_TO = "FIRST TO " + GameManager.instance.scoreToWin;
    }

    // Use this for initialization
    void Start () {
        //FIRST_TO = "FIRST TO " + GameManager.instance.scoreToWin;
		text = GetComponent<Text>();
	}

    // TODO: pull score from GameManager
    //void UpdateText() {
    //    FIRST_TO = "FIRST TO " + GameManager.instance.scoreToWin;
    //}

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
		yield return new WaitForSeconds(5f);
		text.text = EMPTY;
		displaying = false;
	}
}
