using UnityEngine;
using System.Collections;

public class Flame : MonoBehaviour {
	//public GameObject Flames;
	public bool Playonce=false;
	public Transform player;
	public ParticleSystem particalSystem;
	//public GameAudioCOntrollerLevel gA;
	[Header("Player")]
	public float PlayerZ;
	public float PlayerX;
	public float PlayerTotal;
	[Header("Flames")]
	public float FlameZ;
	public float FlameX;
	public float FlamesTotal;
	public GameObject Nozzel;
	public GameObject Flames;
	[Header("Nozzel")]
	Vector3 localCoords;
	Vector3 PlayerCoords;
	float distanceHorizontal;
	float distanceVerticle;
	float diversionAngle;
	Quaternion NozzelRotation;

	Vector3 FlamelocalCoords;
	float FdistanceHorizontal;
	float FdistanceVerticle;
	float FdiversionAngle;
	Quaternion FlameLocalRotation;
	Quaternion FlameWorldRotation;


	// Use this for initialization
	void Start () {
		particalSystem.Stop ();
		player = GameObject.FindWithTag("Player").transform;
	}

	// Update is called once per frame
	void Update () {
		if (!this.animation.isPlaying && !Playonce) {
			localCoords=Nozzel.transform.position;
			PlayerCoords=player.transform.position;
			distanceHorizontal=Vector3.Distance(Nozzel.transform.position, player.transform.position);
			distanceVerticle=transform.InverseTransformPoint(localCoords).y;
			diversionAngle=Mathf.Rad2Deg*Mathf.Atan(distanceVerticle/distanceHorizontal);
			NozzelRotation=Nozzel.transform.localRotation;
			Nozzel.transform.localRotation=Quaternion.Euler(NozzelRotation.x,NozzelRotation.y,diversionAngle);
			particalSystem.Play ();
			particalSystem.startSpeed=(2*( Mathf.Round(Vector3.Distance (Flames.transform.position,player.transform.position)*100)/100));
			Playonce=true;
		}
	
	//Debug.Log ("z player "+player.transform.position.z+" flame "+this.transform.position.z +" total "+(player.transform.position.z-(this.transform.position.z)));
	//Debug.Log ("x player "+player.transform.position.x+" flame "+this.transform.position.x +" total "+(this.transform.position.x-player.transform.position.x));
	
	}
}
