using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultButton : MonoBehaviour {

	EventSystem eventSystem;

	// Use this for initialization
	void Start () {
		eventSystem = EventSystem.current;
		StartCoroutine (HighlightButton ());
	}

	// Update is called once per frame
	void Update () {

	}

	// https://answers.unity.com/questions/1159573/eventsystemsetselectedgameobject-doesnt-highlight.html
	IEnumerator HighlightButton ()
     {
         eventSystem.SetSelectedGameObject (null);
         yield return new WaitForEndOfFrame ();
         eventSystem.SetSelectedGameObject (gameObject);
     }
}
