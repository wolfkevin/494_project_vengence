using System.Collections;
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
    private Vector2 ballHomePositionA = new Vector2(-11.5f, 8f);
    private Vector2 ballHomePositionB = new Vector2(11.5f, 8f);
    private Vector2[] playerHomePositions;
	private Rigidbody jointA;
	private Rigidbody jointB;
	private SpringJoint ballSpring;

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
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;

		jointA = GameObject.FindGameObjectWithTag ("jointA").GetComponent<Rigidbody>();
		jointB = GameObject.FindGameObjectWithTag ("jointB").GetComponent<Rigidbody>();
		ballSpring = ball.GetComponent<SpringJoint> ();

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
		StartCoroutine (Reset (teamThatScored));
    }

    public int GetLeftTeamScore() {
        return leftSideScore;
    }

    public int GetRightTeamScore() {
        return rightSideScore;
    }

    private void ResetBall(Teams teamThatScored) {
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;

        if (teamThatScored == Teams.TeamA) {
			ball.transform.position = ballHomePositionA;
			ballSpring.connectedBody = jointA;
        } else {
			ball.transform.position = ballHomePositionB;
			ballSpring.connectedBody = jointB;
        }

    }

    private void ResetPlayers() {
        for (int i = 0; i < players.Length; ++i) {
            players[i].transform.position = playerHomePositions[i];
            players[i].GetComponent<playerMovement>().ResetPlayer();
        }
    }

    private void GameOver() {
        SceneManager.LoadScene("game_over_scene");
    }

    private void ResetScores() {
        leftSideScore = 0;
        rightSideScore = 0;
    }

    void OnEnable()
    {
        ////Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        //SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "main_scene") {
            ResetScores();
        }
    }

	IEnumerator Reset(Teams teamThatScored)
	{
		ball.SetActive (false);
		Time.timeScale = 0.1f;
		yield return new WaitForSeconds(0.5f);
		ball.SetActive (true);
		Time.timeScale = 1.0f;
		ResetBall(teamThatScored);
		ResetPlayers();
		if (Mathf.Max(leftSideScore, rightSideScore) >= scoreToWin) {
			GameOver();
		}
	}
}
