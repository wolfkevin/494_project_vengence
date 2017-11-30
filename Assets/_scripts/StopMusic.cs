using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusic : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		SoundManager.instance.StopMusic ();
	}
}
