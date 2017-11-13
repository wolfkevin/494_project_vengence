using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public GameObject ball;
    public GameObject[] players;
    public InstructionDisplayer id;
    public int scoreToWin = 7;

    public GameObject blueCelebration;
    public GameObject redCelebration;


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
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;

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
        id.DisplayMessage(InstructionDisplayer.FIRST_TO_7);
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
        //Time.timeScale = 0.1f;
        DisallowPlayerMotion();
        Camera.main.GetComponent<CameraShake>().shakeDuration = 1f;
		yield return new WaitForSeconds(1f);
        if (leftSideScore >= scoreToWin)
        {
            StartCoroutine(WinGame(Teams.TeamA));
        }
        else if (rightSideScore >= scoreToWin)
        {
            StartCoroutine(WinGame(Teams.TeamB));
        } else {
            AllowPlayerMotion();
            ball.SetActive(true);
            Time.timeScale = 1.0f;
            serve.ResetBall(teamThatScored);
            ResetPlayers();
        }


		//if (Mathf.Max(leftSideScore, rightSideScore) >= scoreToWin) {
		//	GameOver();
		//}

        if (Mathf.Max(leftSideScore, rightSideScore) == 6) {
          id.DisplayMessage(InstructionDisplayer.GAME_POINT);
        }
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
}
