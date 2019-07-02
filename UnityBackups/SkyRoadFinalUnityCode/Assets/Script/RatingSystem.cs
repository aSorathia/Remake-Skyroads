using UnityEngine;
using System.Collections;

public class RatingSystem : MonoBehaviour {

	public GameObject Button;
	// Use this for initialization
	void Start () {
		Debug.Log (Button.GetComponent<LevelData> ().ratingValue + "Star");
		//this.transform.FindChild ().GetComponent<UISprite> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
