﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public GameObject ball;
    public GameObject[] players;

    public int scoreToWin = 7;

    private int leftSideScore = 0;
    private int rightSideScore = 0;

    private enum Teams { TeamA, TeamB };
    private Vector2 ballHomePositionA = new Vector2(-16, 7);
    private Vector2 ballHomePositionB = new Vector2(16, 7);
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
        DontDestroyOnLoad(transform.gameObject);
    }


	// Use this for initialization
	void Start () {
        playerHomePositions = new Vector2[players.Length];
        for (int i = 0; i < players.Length; ++i) {
            playerHomePositions[i] = players[i].transform.position;
        }

        ResetBall(Random.value < .5 ? Teams.TeamA : Teams.TeamB);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BallDown(GameObject ball) {
        Teams teamThatScored; 
        if (ball.transform.position.x > 0) {
            teamThatScored = Teams.TeamA;
            leftSideScore += 1;
        } else {
            teamThatScored = Teams.TeamB;
            rightSideScore += 1;
        }
        ResetBall(teamThatScored);
        ResetPlayers();
        if (Mathf.Max(leftSideScore, rightSideScore) >= scoreToWin) {
            GameOver();
        }
    }

    public int GetLeftTeamScore() {
        return leftSideScore;
    }

    public int GetRightTeamScore() {
        return rightSideScore;
    }

    private void ResetBall(Teams teamThatScored) {
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;


        Vector2 ballStartPosition;
        if (teamThatScored == Teams.TeamA) {
            ballStartPosition = ballHomePositionA;
        } else {
            ballStartPosition = ballHomePositionB;
        }

        ball.transform.position = ballStartPosition;
    }

    private void ResetPlayers() {
        for (int i = 0; i < players.Length; ++i) {
            players[i].transform.position = playerHomePositions[i];
            players[i].GetComponent<playerMovement>().ResetPlayer();
        }
    }

    private void GameOver() {
        ResetScores();
        SceneManager.LoadScene("game_over_scene");
    }

    private void ResetScores() {
        leftSideScore = 0;
        rightSideScore = 0;
    }
}
