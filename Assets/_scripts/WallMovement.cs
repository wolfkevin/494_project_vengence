using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class WallMovement : MonoBehaviour {
  private InputDevice inputDevice;
  private CapsuleCollider collider;
  private PlayerMovement pm;
  private Rigidbody rb;
  private float xInput;
  private float yInput;


 private Vector3 v3OrgPos;
 private float orgScale;
 private float endScale;
 public float speed = 5f;

 private GameObject bodyPivot;

 private bool walled;

    // Use this for initialization
    void Start()
    {
        inputDevice = GetComponentInParent<PlayerInputDevice>().inputDevice;
        rb = GetComponentInParent<Rigidbody>();
        pm = GetComponentInParent<PlayerMovement>();
        collider = GetComponent<CapsuleCollider>();

        walled = false;

       v3OrgPos = transform.position;
       orgScale = transform.localScale.x;
       endScale = orgScale;

       bodyPivot = transform.parent.gameObject;

    }

    void Update(){
      if (inputDevice.Action2.IsPressed && pm.IsGrounded() && !walled){
        rb.velocity = new Vector2(0f, rb.velocity.y);
        walled = true;
        pm.DisallowMotion();
        collider.radius = .25f;
        collider.height = 1;
        xInput = inputDevice.LeftStickX;
        yInput = inputDevice.LeftStickY;
        if (yInput > .75f){
          WallUp();
        // } else if (xInput > .75f) {
        //   WallRight();
        // } else if (xInput < -.75f) {
        //   WallLeft();
        } else {
          WallMiddle();
        }
      } else if (inputDevice.Action2.WasReleased && walled){
        walled = false;
        pm.AllowMotion();
        ResetPlayerBody();
      }
    }

    void WallUp(){
      transform.parent.parent.position = new Vector3(transform.parent.parent.position.x, transform.parent.parent.position.y + 1f);
      transform.localScale = new Vector3(1, 4, 2);
      collider.direction = 1; //set capsule direction to y-axis or 1
    }

    // void WallLeft(){
    //   transform.localScale = new Vector3(4, 1, 2);
    // }

    void WallRight(){
      Debug.Log("wall right");
        transform.SetParent(bodyPivot.transform.parent);
        bodyPivot.transform.position = Vector3.left;
        // transform.SetParent(bodyPivot.transform);
        // transform.position = Vector3.right;
        // bodyPivot.transform.localScale = new Vector3(2f, .5f);
        //
        // collider.direction = 0; //set capsule direction to x-axis or 0
        // collider.height = 1;
      // transform.localScale = new Vector3(4, 1, 2);
    }

    void WallMiddle(){
      transform.localScale = new Vector3(4, 1, 2);
      collider.direction = 0; //set capsule direction to x-axis or 0
    }

    void ResetPlayerBody(){
      collider.height = 0;
      collider.radius = .5f;
      transform.localScale = new Vector3(2, 2, 2);
    }

}
