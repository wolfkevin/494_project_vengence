﻿using UnityEngine;

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
        Debug.Log(other.tag);

        if (other.CompareTag("player")) {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            PlayerInputDevice pid = other.GetComponent<PlayerInputDevice>();

            if (pm.IsDashing()) {
                // dashes consume charge regardless of team
                GameData.BoxScore[pid.playerNumber].dashHits += 1;
            } else {
                GameData.BoxScore[pid.playerNumber].hits += 1;
            }
            Debug.Log(GameData.BoxScore[pid.playerNumber]);
        }

        if (other.CompareTag("ground")) {
            PlayerInputDevice pid = cb.LastHitBy().GetComponent<PlayerInputDevice>();

            if (transform.position.x > 0)
            {
                if (pid.playerNumber == 0 || pid.playerNumber == 2)
                {
                    GameData.BoxScore[pid.playerNumber].points += 1;
                }
            }
            else if (transform.position.x < 0)
            {
                if (pid.playerNumber == 1 || pid.playerNumber == 3)
                {
                    GameData.BoxScore[pid.playerNumber].points += 1;
                }
            }
            Debug.Log(GameData.BoxScore[pid.playerNumber]);
        }
    }
}