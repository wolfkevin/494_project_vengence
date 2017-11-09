using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionKnockback : MonoBehaviour {

	public GameObject[] players;
	Vector3 knockDirection;

	public void ExplodeAndKnockBack(){
		Vector3 explosionPosition = this.gameObject.transform.position;
		for (int i = 0; i < players.Length; i++) {
			knockDirection = (players [i].gameObject.transform.position - explosionPosition);
			players [i].GetComponent<Rigidbody> ().AddForce (knockDirection.normalized * 2500);
			players [i].GetComponent<Rigidbody> ().AddForce (new Vector3 (0, 2500, 0));

		}
	}
}
