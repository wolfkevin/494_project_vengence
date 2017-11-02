using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private int leftSideScore = 0;
    private int rightSideScore = 0;

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
    }
}
