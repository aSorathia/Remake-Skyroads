using UnityEngine;
using System.Collections;

public class ShakerScript : MonoBehaviour {

	public GameObject LevelStatsShake;
	public bool shaked=false;
	public float shakeX;
	public float shakeY;
	public float duration;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Shaker(){
		iTween.ShakePosition (LevelStatsShake, iTween.Hash ("x",shakeX,"y",shakeY,"time",duration));
	}
}
