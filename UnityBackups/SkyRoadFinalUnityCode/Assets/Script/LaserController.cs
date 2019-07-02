using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour {

	public Transform player;
	public ParticleSystem particalSystem;
	public float distanceHover;
	public bool RaycastPlayOnce = false;
	public bool particalSystemOn = false;
	public Transform Blob;
	// Use this for initialization
	void Start () {

		particalSystem.Stop ();
		player = GameObject.FindWithTag("Player").transform;
		Blob.animation.Stop ();

	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (player);
		if(Physics.Raycast(this.transform.position,-Vector3.up,distanceHover) && !RaycastPlayOnce){
			Destroy(this.gameObject.GetComponent<Rigidbody>());
			Blob.animation.Play();
			RaycastPlayOnce=true;
		}
		if (!Blob.animation.isPlaying && !particalSystemOn && RaycastPlayOnce) {
			particalSystem.Play();
			particalSystemOn=true;
		}
	}
}
