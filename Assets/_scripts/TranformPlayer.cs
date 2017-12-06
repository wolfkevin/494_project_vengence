using System.Collections;
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

	void Start() {
		grandfather = transform.parent.parent;
		capsule = this.GetComponent<CapsuleCollider>();
		inputDevice = GetComponentInParent<PlayerInputDevice>().inputDevice;
		pm = GetComponentInParent<PlayerMovement>();
		rb = GetComponentInParent<Rigidbody>();
		aSources = GetComponents<AudioSource>();
		splashSound = aSources[0];
		upSound = aSources[1];
	}

	void Update() {
        if (inputDevice == null) {
            return;
        }

		if (!switching
            && !walled
            && inputDevice.Action2.IsPressed
            //&& pm.IsGrounded()
            && !pm.OnPlayer()) {
			rb.velocity = new Vector2(0f, rb.velocity.y);
			walled = true;
            pm.DisallowMotion();
            pm.FixPosition();
			switching = true;
			StartCoroutine(Swap());

		} else if (inputDevice.Action2.WasReleased && walled){
			StopCoroutine("Swap");
			walled = false;
			switching = false;
            pm.AllowMotion();
            pm.UnfixPosition();
			StartCoroutine(ResetPlayerBody());
		}
	}

	IEnumerator Swap() {
        pm.KillDash();

		switching = true;

		xInput = inputDevice.LeftStickX;
		yInput = inputDevice.LeftStickY;
		Vector3 startPos = grandfather.position;
		if (Mathf.Abs(yInput) > .6f){
			capsule.direction = 1;
			Vector3 endPos;
			if (yInput < 0) {
				 Vector3 down = grandfather.TransformDirection(Vector3.down);
				 if (Physics.Raycast(grandfather.position, down, 1)){
					 Debug.Log("down ray hit");
					 yield break;
				 }
				endPos = new Vector3(startPos.x, startPos.y - 2f, startPos.z);
			} else {
				endPos = new Vector3(startPos.x, startPos.y + 2f, startPos.z);
			}
			upSound.Play();
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


    //IEnumerator Flash() {
    //    while (true) {
    //        GetComponent<MeshRenderer>().enabled = false;
    //        yield return new WaitForSeconds
    //    }
    //}

}
