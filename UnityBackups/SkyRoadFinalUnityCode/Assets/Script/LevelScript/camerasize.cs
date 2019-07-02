using UnityEngine;
using System.Collections;

public class camerasize : MonoBehaviour {
	public Camera cameraBg;
	// Use this for initialization
	void Start () {
		cameraBg.orthographicSize = Screen.height ;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
