using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getTrailRendererColor : MonoBehaviour {

	private ParticleSystemRenderer psr;
	private Material material;

	// Use this for initialization
	void Start () {
		psr = this.GetComponent<ParticleSystemRenderer> ();
		material = this.GetComponent<MeshRenderer> ().material;
		psr.material = material;

	}
}
