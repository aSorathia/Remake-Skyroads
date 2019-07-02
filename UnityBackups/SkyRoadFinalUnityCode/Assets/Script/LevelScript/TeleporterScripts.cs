using UnityEngine;
using System.Collections;

public class TeleporterScripts : MonoBehaviour {
	public TweenPosition tw;
	public ParticleSystem ps;
	public bool played=false;
	public GameObject Vehicle;
	public bool isTweenPositionComplete=false;
	bool isVehicleCreated;
	public GameObject UIRoot;
	bool isEverythingActivated=false;
	public GameObject Ship;
	bool isAllTweenComplete=false;
	bool isWaitComplete=false;
	public GameObject Overlay;
	public GameObject LevelStats;
	public GameObject InfoDisplayBox;
	public GameObject portableSettings;
	public GameObject Settings;
	public GameObject colliderIdentifier;
	// Use this for initialization
	void Start () {
	//	ps.Stop ();
	}
	
	// Update is called once per frame
	public void isTweenFinished(){
		StartCoroutine (waitforWhile());
	}
	IEnumerator waitforWhile(){
		isTweenPositionComplete = true;
		yield return new WaitForSeconds(4f);
		isWaitComplete = true;
	}
	void Update(){
		if(isTweenPositionComplete && !isVehicleCreated){
			Vehicle.SetActive(true);
			isVehicleCreated=true;
			//UIRoot.SetActive (true);
			//Vehicle.SetActive (true);
			//Enable vehicle compnent one by one
		}

		if (isVehicleCreated && !isAllTweenComplete && isWaitComplete) {
			ps.Stop ();
			tw.duration=1f;
			tw.PlayReverse();
			isAllTweenComplete=true;
		}

		if(Vehicle.activeSelf && isAllTweenComplete && !isEverythingActivated){
			Debug.Log("Vehicle is Active");
			Ship.GetComponent<PlayerMovement>().enabled=true;
			Ship.GetComponent<DeathndGoal>().enabled=true;
			Overlay.SetActive (true);
			LevelStats.SetActive (true);
			InfoDisplayBox.SetActive (true);
			portableSettings.SetActive (true);
			Settings.SetActive (true);
			isEverythingActivated=true;
			colliderIdentifier.SetActive(true);
			//Destroy(this.gameObject);
			Debug.Log ("All Set");
		}


	}
}
