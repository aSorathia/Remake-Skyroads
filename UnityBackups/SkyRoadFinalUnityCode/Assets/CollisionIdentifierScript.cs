using UnityEngine;
using System.Collections;

public class CollisionIdentifierScript : MonoBehaviour {
	public GameObject Player;
	public bool dead;
	public bool goal;
	public PlayerMovement PM;
	GameObject speedOMeter;
	private EnergyBar speedbar;
	CameraFollow cameraFollow;
	public DeathndGoal DG;
	public Vector3 Offsets;
	public float CollisionSpeed;
	void Start () {
		dead = false;
		goal = false;
		cameraFollow = Camera.main.GetComponent<CameraFollow> ();
		speedOMeter = GameObject.FindWithTag ("SpeedOMeter");
		speedbar = speedOMeter.GetComponent<EnergyBar>();
	}
	/*void Update(){
		if(!dead){
			transform.position=new Vector3(Player.transform.position.x+Offsets.x,
			                               Player.transform.position.y+Offsets.y,
			                               Player.transform.position.z+Offsets.z);
		}
	}*/
	void OnCollisionEnter(Collision collision){
		/*ContactPoint contact = collision.contacts[0];

		if (contact.normal.y < 0.9f && speedbar.valueCurrent > CollisionSpeed && !this.dead && !this.goal){
			//cameraFollow.exploded=true;
			Destroy(Player.GetComponent<Rigidbody>());
			//DG.Explode();
		}*/
		print ("Collision has occured in cif child");
	
	}
	void OnTriggerEnter(){
		print ("Trigger has occured in cif");
	}

	// Use this for initialization
	/*void Start () {
		cameraFollow = Camera.main.GetComponent<CameraFollow> ();
		speedOMeter = GameObject.FindWithTag ("SpeedOMeter");
		speedbar = speedOMeter.GetComponent<EnergyBar>();
		dead = false;
		goal = false;
		Player = GameObject.FindWithTag ("Player");
		startx=transform.position.x;
		starty=transform.position.y;
		startz=1.030352f;
	}*/
	// Update is called once per frame
	void OnCollisionEnter(){
		/*
		if (speedbar.valueCurrent > DG.CollisionSpeed && !DG.death && !DG.goal){
			cameraFollow.exploded=true;
			DG.Explode();
		}
		print ("Collision Hashtable occured");
		print (speedbar.valueCurrent);
		*/
		//print("Points colliding: " + collision.contacts.Length);
		//print("First normal of the point that collide: " + collision.contacts[0].normal);
	}
}
