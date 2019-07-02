using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {
	public ParticleSystem Triggeremitter;

	// Use this for initialization
	void Start () {
		Triggeremitter.Stop ();
	}
}
