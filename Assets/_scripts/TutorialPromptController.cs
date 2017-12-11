using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class TutorialPromptController : MonoBehaviour {

    bool didJump = false;
    bool didDash = false;
    bool didTramp = false;
    bool didWall = false;
    bool isReady = false;

    public GameObject AbuttonPrompt;
    public GameObject BbuttonPrompt;
    public GameObject LbuttonPrompt;
    public GameObject holdPrompt;
    public GameObject upButtonPrompt;
    public GameObject checkPrompt;

    InputDevice inputDevice;
    PlayerMovement pm;
    TranformPlayer tp;
    Transform body;

	// Use this for initialization
	void Start () {
        inputDevice = GetComponent<PlayerInputDevice>().inputDevice;
        pm = GetComponent<PlayerMovement>();
        tp = GetComponentInChildren<TranformPlayer>();
        body = transform.Find("BodyPivot").Find("Body");

        AbuttonPrompt.SetActive(true);
        BbuttonPrompt.SetActive(false);
        LbuttonPrompt.SetActive(false);
        holdPrompt.SetActive(false);
        upButtonPrompt.SetActive(false);
        checkPrompt.SetActive(false);

        tp.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (tp.IsSwitching() || body.localScale.x > 5 || body.localScale.y > 5) {
            AbuttonPrompt.SetActive(false);
            BbuttonPrompt.SetActive(false);
            LbuttonPrompt.SetActive(false);
            holdPrompt.SetActive(false);
            upButtonPrompt.SetActive(false);
            checkPrompt.SetActive(false);
        }
        else if (!didJump) {
            AbuttonPrompt.SetActive(true);
            BbuttonPrompt.SetActive(false);
            LbuttonPrompt.SetActive(false);
            holdPrompt.SetActive(false);
            upButtonPrompt.SetActive(false);
            if (!pm.IsGrounded()) {
                didJump = true;
            } 
        }
        else if (!didDash) {
            if (!pm.IsGrounded()) {
                AbuttonPrompt.SetActive(true);
                BbuttonPrompt.SetActive(false);
                LbuttonPrompt.SetActive(false);
                holdPrompt.SetActive(true);
                upButtonPrompt.SetActive(false);
                if (pm.IsCharging())
                {
                    AbuttonPrompt.SetActive(false);
                    BbuttonPrompt.SetActive(false);
                    LbuttonPrompt.SetActive(true);
                    holdPrompt.SetActive(false);
                    upButtonPrompt.SetActive(false);
                }
                else if (pm.DidDashOnce())
                {
                    didDash = true;
                    tp.enabled = true;
                    AbuttonPrompt.SetActive(false);
                    BbuttonPrompt.SetActive(false);
                    LbuttonPrompt.SetActive(false);
                    holdPrompt.SetActive(false);
                    upButtonPrompt.SetActive(false);
                }
            }
            else {
                AbuttonPrompt.SetActive(true);
                BbuttonPrompt.SetActive(false);
                LbuttonPrompt.SetActive(false);
                holdPrompt.SetActive(false);
                upButtonPrompt.SetActive(false);
            }
        }
        else if (!didTramp) {
            AbuttonPrompt.SetActive(false);
            BbuttonPrompt.SetActive(true);
            LbuttonPrompt.SetActive(false);
            holdPrompt.SetActive(false);
            upButtonPrompt.SetActive(false);

            if (tp.DidTrampOnce()) {
                didTramp = true;
                AbuttonPrompt.SetActive(false);
                BbuttonPrompt.SetActive(false);
                LbuttonPrompt.SetActive(false);
                holdPrompt.SetActive(false);
                upButtonPrompt.SetActive(false);
            }
        }
        else if (!didWall) {
            AbuttonPrompt.SetActive(false);
            BbuttonPrompt.SetActive(true);
            LbuttonPrompt.SetActive(false);
            holdPrompt.SetActive(false);
            upButtonPrompt.SetActive(true);

            if (tp.DidWallOnce()) {
                didWall = true;
                isReady = true;
                AbuttonPrompt.SetActive(false);
                BbuttonPrompt.SetActive(false);
                LbuttonPrompt.SetActive(false);
                holdPrompt.SetActive(false);
                upButtonPrompt.SetActive(false);
            }
        }
        else if (isReady) {
            checkPrompt.SetActive(true);
        }
	}

    public bool IsReady() {
        return isReady;
    }
}
