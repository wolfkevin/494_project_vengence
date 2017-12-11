using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsDisplayer : MonoBehaviour
{
	public int pid;
	
	Text statLine;

	// Use this for initialization
	void Start () {
		statLine = GetComponent<Text>();
		try
		{
			statLine.text = GameData.BoxScore[pid].ToString();
		}
		catch (NullReferenceException e)
		{
		}
		catch (KeyNotFoundException e)
		{	
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
