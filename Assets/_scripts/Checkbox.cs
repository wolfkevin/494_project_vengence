using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkbox : MonoBehaviour {

	SpriteRenderer spr;

	// Use this for initialization
	void Start () {
		spr = this.GetComponent<SpriteRenderer> ();
	}


	public void SetCheckboxImage(bool val){
		spr.enabled = val;
	}
}
