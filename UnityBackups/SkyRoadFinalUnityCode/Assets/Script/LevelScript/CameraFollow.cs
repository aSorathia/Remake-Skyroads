using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject Player;
	public Vector3  positionoffset = Vector3.zero;
	public float startx;
	public float startz;
	public float starty;
	public Vector3 currentPos;
	public Quaternion currentRot;
	public bool dead;
	public bool goal;
	public bool exploded;
	public bool FlameThrowerAct;
	public bool isdeadAudioPlayed=false;
	public bool isExploedAudioPlayed=false;
	public GameAudioCOntrollerLevel gA;
	public bool PlayerCreated=false;
	public GameObject OriginalPlayer;
	public GameObject tempPlayer;
	public Vector3 AxisOfRotation;
	public float speedOfotation;
	public bool CamRotate;
	public bool PositionMoved;
	public Vector3 lastPosition;
	public Quaternion lastRotaion;
	public Vector3 movePosition;
	public float MoveSpeed;
	public float damping = 6.0f;
	public Vector3 playerposition;
	// Use this for initialization
	void Start () {

		CamRotate=false;
		PositionMoved = false;
		dead = false;
		goal = false;
		exploded = false;
		Player = GameObject.FindWithTag ("Player");
		if (!Player) {
			Debug.Log ("Not attached");		
		}
		Debug.Log ("Current Attached player "+Player.name);
		positionoffset = transform.position - Player.transform.position;
		//positionoffset.x=transform.position.x;
		//positionoffset.y=transform.position.y;*/
		startx=transform .position.x;
		starty=transform .position.y;
		startz=transform.position.z-Player.transform .position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerCreated) {
			Player=OriginalPlayer;
			PlayerCreated=false;	
			Debug.Log ("Current Attached player "+Player.name);
			Destroy (tempPlayer);
		}
		if (!dead && Player) {
			transform.position = new Vector3 (startx, starty, startz + Player.transform.position.z);
			currentPos=transform.position;
			currentRot=transform.rotation;
		}
		if(dead && exploded && !isExploedAudioPlayed){
			audio.clip=gA.FXClips[1];
			audio.Play();
			isExploedAudioPlayed=true;
		}
		if(dead && !exploded && !isdeadAudioPlayed){
			audio.clip=gA.FXClips[2];
			audio.Play();
			isdeadAudioPlayed=true;
		}
		if(dead && FlameThrowerAct && !PositionMoved){
			float steps=MoveSpeed*Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, movePosition, steps);
			var rotation = Quaternion.LookRotation(playerposition - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
			if(transform.position==movePosition){
				PositionMoved=true;
			}
		}
		if(dead && FlameThrowerAct && PositionMoved){
			transform.RotateAround(playerposition,AxisOfRotation,speedOfotation*Time.deltaTime);
		}
	}
}
