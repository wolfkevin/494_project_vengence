﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;



public class TranformPlayer : MonoBehaviour {


	public float switchTime = .1f;
    private float xInput;
    private float yInput;

	InputDevice inputDevice;

	float minCapsuleRadius = .25f;
	float maxCapsuleRadius = .5f;

	float minCapsuleLength = 0f;
	float maxCapsuleLength = 1f;

	float sphereRadius = .5f;
	float sphereLength = 2f;

	float minSphereLength = 1f;
	float maxSphereLength = 6f;


	public AnimationCurve curve;

	CapsuleCollider capsule;
	PlayerMovement pm;
	Rigidbody rb;

	bool switching = false;
	bool walled = false;

	Transform grandfather;

	AudioSource splashSound;
	AudioSource upSound;
	AudioSource[] aSources;


	    bool isMainScene;

	void Start() {
		grandfather = transform.parent.parent;
		capsule = this.GetComponent<CapsuleCollider>();
		inputDevice = GetComponentInParent<PlayerInputDevice>().inputDevice;
		pm = GetComponentInParent<PlayerMovement>();
		rb = GetComponentInParent<Rigidbody>();
		aSources = GetComponents<AudioSource>();
		splashSound = aSources[0];
		upSound = aSources[1];
		if (SceneManager.GetActiveScene().name == "main_scene"){
			isMainScene = true;
		} else {
			isMainScene = false;
		}
	}

	void Update() {
      if (inputDevice == null) {
          return;
      }

		if (!switching && !walled && inputDevice.Action2.IsPressed && pm.IsGrounded() && (!isMainScene || (grandfather.position.y < 1.6f))) {
			rb.velocity = new Vector2(0f, rb.velocity.y);
			walled = true;
            //pm.FixPosition();
			pm.DisallowMotion();
			switching = true;
			StartCoroutine(Swap());

		} else if (inputDevice.Action2.WasReleased && walled){
			StopCoroutine("Swap");
			walled = false;
			switching = false;
            //pm.UnfixPosition();
			pm.AllowMotion();
			StartCoroutine(ResetPlayerBody());
		}
	}

	IEnumerator Swap() {
		switching = true;

		xInput = inputDevice.LeftStickX;
		yInput = inputDevice.LeftStickY;
		Vector3 startPos = grandfather.position;
		if (yInput > .6f){
			capsule.direction = 1;
			upSound.Play();
			Vector3 endPos = new Vector3(startPos.x, startPos.y + 2f, startPos.z);
			for (float t = 0; t < switchTime; t += Time.deltaTime) {
				float p = t / switchTime;
				p = curve.Evaluate(p);
				Vector3 scale = transform.localScale;
				grandfather.position = Vector3.Lerp(startPos, endPos, p);
				scale.y = Mathf.Lerp(sphereLength, maxSphereLength, p);
				scale.x = Mathf.Lerp(sphereLength, minSphereLength, p);
				capsule.radius = Mathf.Lerp(maxCapsuleRadius, minCapsuleRadius, p);
				capsule.height = Mathf.Lerp(minCapsuleLength, maxCapsuleLength, p);

				transform.localScale = scale;

				yield return null;
			}
		} else {
			capsule.direction = 0;
			Vector3 endPos;
			if (xInput > .6f) {
				 Vector3 right = grandfather.TransformDirection(Vector3.right);
				 if (Physics.Raycast(grandfather.position, right, 2)){
					 Debug.Log("right ray hit");
					 yield break;
				 }
				endPos = new Vector3(startPos.x + 2f, startPos.y - .5f, startPos.z);
			} else if (xInput < -.6f) {
				Vector3 left = grandfather.TransformDirection(Vector3.left);
			 	if (Physics.Raycast(grandfather.position, left, 2)){
				 yield break;
			 }
				endPos = new Vector3(startPos.x - 2f, startPos.y - .5f, startPos.z);
			} else {
				endPos = new Vector3(startPos.x, startPos.y - .5f, startPos.z);
			}
			splashSound.Play();
			for (float t = 0; t < switchTime; t += Time.deltaTime) {
				float p = t / switchTime;
				p = curve.Evaluate(p);
				Vector3 scale = transform.localScale;
				grandfather.position = Vector3.Lerp(startPos, endPos, p);
				scale.x = Mathf.Lerp(sphereLength, maxSphereLength, p);
				scale.y = Mathf.Lerp(sphereLength, minSphereLength, p);
				capsule.radius = Mathf.Lerp(maxCapsuleRadius, minCapsuleRadius, p);
				capsule.height = Mathf.Lerp(minCapsuleLength, maxCapsuleLength, p);

				transform.localScale = scale;

				yield return null;
			}
		}
		switching = false;
	}

	IEnumerator ResetPlayerBody(){
		Vector3 scale = transform.localScale;
		float curSphereX = scale.x;
		float curSphereY = scale.y;
		for (float t = 0; t < switchTime; t += Time.deltaTime) {
			float p = t / switchTime;
			p = curve.Evaluate(p);
			Vector3 transformScale = transform.localScale;
			transformScale.x = Mathf.Lerp(curSphereX, sphereLength, p);
			transformScale.y = Mathf.Lerp(curSphereY, sphereLength, p);
			capsule.radius = Mathf.Lerp(minCapsuleRadius, maxCapsuleRadius, p);
			capsule.height = Mathf.Lerp(maxCapsuleLength, minCapsuleLength, p);

			transform.localScale = transformScale;

			yield return null;
		}
        capsule.radius = .5f;
        capsule.height = .5f;
        transform.localScale = new Vector3(2, 2, 2);
		switching = false;
	}
	// IEnumerator Wal()

}
