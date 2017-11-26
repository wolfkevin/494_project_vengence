using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public GameObject ball;
    public GameObject[] players;

    public int scoreToWin = 7;

    public GameObject blueCelebration;
    public GameObject redCelebration;

    private InstructionDisplayer id;

    private int leftSideScore = 0;
    private int rightSideScore = 0;

    public enum Teams { TeamA, TeamB };
    private Vector2[] playerHomePositions;
	private ServeBall serve;

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
        id = GameObject.Find("Instructions").GetComponent<InstructionDisplayer>();
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;

        players = GameObject.FindGameObjectsWithTag("player");

        playerHomePositions = new Vector2[players.Length];
        for (int i = 0; i < players.Length; ++i) {
            playerHomePositions[i] = players[i].transform.position;
        }
		serve = ball.GetComponent<ServeBall> ();
        ResetScores();
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

    private void ResetPlayers() {
        for (int i = 0; i < players.Length; ++i) {
            players[i].transform.position = playerHomePositions[i];
            players[i].GetComponent<Rigidbody>().velocity = Vector2.zero;
            players[i].GetComponent<PlayerMovement>().ResetPlayer();
        }
    }

    private IEnumerator WinGame(Teams winningTeam) {
        switch(winningTeam) {
            case Teams.TeamA:
                blueCelebration.SetActive(true);
                break;
            case Teams.TeamB:
                redCelebration.SetActive(true);
                break;
            default:
                break;

        }
        yield return new WaitForSeconds(3f);
        GameOver();
    }

    private void GameOver() {
        GameData.LeftSideScore = leftSideScore;
        GameData.RightSideScore = rightSideScore;
        SceneManager.LoadScene("game_over_scene");
    }

    private void ResetScores() {
        Debug.Log("resetting scores");
        leftSideScore = 0;
        rightSideScore = 0;
        id = GameObject.Find("Instructions").GetComponent<InstructionDisplayer>();
        id.DisplayMessage(id.FIRST_TO);
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
        //Time.timeScale = 0.1f;
        DisallowPlayerMotion();
        Camera.main.GetComponent<CameraShake>().shakeDuration = 1.5f;
		yield return new WaitForSeconds(1.5f);
        if (leftSideScore >= scoreToWin && WonByTwo())
        {
            StartCoroutine(WinGame(Teams.TeamA));
        }
        else if (rightSideScore >= scoreToWin && WonByTwo())
        {
            StartCoroutine(WinGame(Teams.TeamB));
        } else {
            AllowPlayerMotion();
            ball.SetActive(true);
            Time.timeScale = 1.0f;
            serve.ResetBall(teamThatScored);
            ResetPlayers();
        }

	    // Display message 
	    if (Mathf.Max(leftSideScore, rightSideScore) >= scoreToWin && !WonByTwo()) {
	        id.DisplayMessage(id.WIN_BY_TWO);
	    } else if (Mathf.Max(leftSideScore, rightSideScore) == scoreToWin - 1) {
            id.DisplayMessage(id.GAME_POINT);
        }
	}

    private bool WonByTwo()
    {
        return Mathf.Abs(leftSideScore - rightSideScore) >= 2f;
    }

    //private IEnumerator

    private void DisallowPlayerMotion() {
        for (int i = 0; i < players.Length; ++i) {
            players[i].GetComponent<PlayerMovement>().DisallowMotion();
        }
    }

    private void AllowPlayerMotion()
    {
        for (int i = 0; i < players.Length; ++i)
        {
            players[i].GetComponent<PlayerMovement>().AllowMotion();
        }
    }

    public GameObject[] GetPlayers() {
        return players;
    }

    public int GetScoreToWin() {
        return scoreToWin;
    }
}
