using UnityEngine;
using System.Collections;

public class BoosterAttachment : MonoBehaviour {
	public Transform player;
	public Vector3 boosterAttachmentPos;
	public bool isTweenComplete=true;
	public bool isPositonSet=false;
	public ParticleSystem igniter;
	public ParticleSystem Booster;
	public ParticleSystem airBlast1;
	public ParticleSystem airBlast2;
	public Transform pointA;
	public Transform pointB;
	public bool detach =false;
	public bool phase1=false;
	public bool phase2=false;
	DeathndGoal DGSCript;
	PlayerMovement Pm;
	void Start () {
		player = GameObject.FindWithTag("Player").transform;
		DGSCript = player.GetComponent<DeathndGoal> ();
		Pm = player.GetComponent<PlayerMovement> ();
		DGSCript.isBoosterOnSceen=true;
		Pm.isBoosterOnSceen = true;
		pointA = GameObject.Find ("pointA").transform;
		pointB = GameObject.Find ("pointB").transform;

		this.transform.parent = player;
		boosterAttachmentPos = player.FindChild ("BoosterAttacherShip").localPosition;
		iTween.MoveTo(this.gameObject,iTween.Hash("position",boosterAttachmentPos,"islocal", true,"time",0.5f,"oncomplete","MovePosition"));
		igniter.Stop (); 
		Booster.Stop ();
		airBlast1.Stop ();
		airBlast2.Stop ();

	}

	void MovePosition(){
		isTweenComplete = true;
	}

	public void Update(){
		if (!isPositonSet) {
			//transform.localPosition = Vector3.MoveTowards (transform.localPosition, boosterAttachmentPos, 3f * Time.deltaTime);
			if(transform.localPosition==boosterAttachmentPos)
			{
				isPositonSet=true;
				StartCoroutine(onAttachment());

			}
		}
		if (detach && !phase1) {
			transform.localPosition = Vector3.MoveTowards (transform.localPosition, pointA.localPosition, 20f * Time.deltaTime);	
			if(transform.position == pointA.position){
				phase1=true;
				Debug.Log ("Phase1 complete");
			}
		}
		if (phase1 && !phase2) {
			transform.localPosition = Vector3.MoveTowards (transform.localPosition, pointB.localPosition, 20f * Time.deltaTime);	
			if(transform.position == pointB.position){
				phase2=true;
				Debug.Log ("Phase2 complete");
			}
		}
		if (phase1 && phase2) {
			DGSCript.isBoosterOnSceen=false;
			Pm.isBoosterOnSceen=false;	
		}
	}

	IEnumerator onAttachment()
	{
		igniter.Play();
		Booster.Play();
		//Increase Speed
		yield return new WaitForSeconds(5);
		BoosterDetach ();
	}
 	void BoosterDetach(){
		this.transform.parent = GameObject.FindWithTag("MainCamera").transform;
		detach = true;
		Debug.Log ("Detach");
		iTween.RotateTo (this.gameObject, iTween.Hash ("x",296.8f,"time",3f));
		airBlast1.Play ();
		airBlast2.Play ();
	}
}
