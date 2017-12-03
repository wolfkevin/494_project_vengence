using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
public class BoxScore : MonoBehaviour
{
    private ChargedBall cb;

    // Use this for initialization
    void Start ()
    {
        cb = GetComponent<ChargedBall>();
        ResetBoxScores();
    }
	
    // Update is called once per frame
    void Update () {
		
    }

    private void ResetBoxScores()
    {
		
        GameData.BoxScore[0] = new Stats();
        GameData.BoxScore[1] = new Stats();
        GameData.BoxScore[2] = new Stats();
        GameData.BoxScore[3] = new Stats();
    }
	
    void OnCollisionEnter(Collision collision) {
        GameObject other = collision.gameObject;

        if (other.CompareTag("player")) {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            PlayerInputDevice pid = other.GetComponent<PlayerInputDevice>();

            if (pm.IsDashing()) {
                // dashes consume charge regardless of team
                GameData.BoxScore[pid.playerNumber].dashHits += 1;
            } else {
                GameData.BoxScore[pid.playerNumber].hits += 1;
            }
        }

        if (other.CompareTag("ground")) {
            PlayerInputDevice pid = cb.LastHitBy().GetComponent<PlayerInputDevice>();
            GameData.BoxScore[pid.playerNumber].points += 1;
        }
    }
}