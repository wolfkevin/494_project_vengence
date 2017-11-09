using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionKnockback : MonoBehaviour {

    private GameObject[] players;
	private Vector3 knockDirection;
    private float knockStrength = 1000f;

    private void Start()
    {
        players = GameManager.instance.GetPlayers();
    }

    public void ExplodeAndKnockBack(){
		Vector3 explosionPosition = this.gameObject.transform.position;
		for (int i = 0; i < players.Length; i++) {
			knockDirection = (players [i].gameObject.transform.position - explosionPosition);
            players [i].GetComponent<Rigidbody> ().AddForce (knockDirection.normalized * knockStrength);
            players [i].GetComponent<Rigidbody> ().AddForce (new Vector3 (0, knockStrength, 0));

		}
	}
}
