using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public GameObject ball;
    public GameObject[] players;

    private int leftSideScore = 0;
    private int rightSideScore = 0;

    private Vector2 ballHomePosition;
    private Vector2[] playerHomePositions;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(transform.gameObject);
    }


	// Use this for initialization
	void Start () {
        ballHomePosition = ball.transform.position;

        playerHomePositions = new Vector2[players.Length];
        for (int i = 0; i < players.Length; ++i) {
            playerHomePositions[i] = players[i].transform.position;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BallDown(GameObject ball) {
        if (ball.transform.position.x > 0) {
            leftSideScore += 1;
        } else {
            rightSideScore += 1;
        }
        ResetBall();
        ResetPlayers();
    }

    public int GetLeftTeamScore() {
        return leftSideScore;
    }

    public int GetRightTeamScore() {
        return rightSideScore;
    }

    private void ResetBall() {
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.transform.position = ballHomePosition;
    }

    private void ResetPlayers() {
        for (int i = 0; i < players.Length; ++i) {
            players[i].transform.position = playerHomePositions[i];
        }
    }


}
