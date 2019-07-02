using UnityEngine;
using System.Collections;

public class DeathndGoal : MonoBehaviour {

	//set Stage and level
	public int stage;
	public int level;
	public bool goal;
	public bool death;
	int deathCount;
	//Set collision Speed
	public GameObject Blast; //ExplosionObject
	public GameObject FlameThrowerObject;
	GameObject speedOMeter;
	private EnergyBar speedbar;
	public GameObject Boosters;
	public float BoosterHeight;
	public GameObject StickyTurret;
	public float SizeX=3.753f;
	public float SizeZ=12.5f;
	private float partSizeZ ;
	private float partSizeX ;
	CameraFollow cameraFollow;
	//public TweenPosition Overlay;
	//public TweenPosition LevelStats;
	public Vector2 star5;
	public Vector2 star4;
	public Vector2 star3;
	public Vector2 star2;
	public Vector2 star1;
	bool playOnce=false;
	//bool deathexploded=false;
	Timer time;
	int foundKey=0;
	bool isOverlayPlayed=false;
	public ParticleSystem normalKeyAnimation;
	public ParticleSystem KeyTriggeranimation;
	public GameObject Key;
	public InfoBoxANimation InfoBox;
	public GameObject ExplosionBurner;
	public GameObject fireBurner;
	public float CollisionSpeed;
	/*public ShipAudioController shipaudioController;
	public GameAudioCOntrollerLevel gA;

	public GameObject StickyBlock;
	public GameObject Hovert;
	*/public bool isBoosterOnSceen=false;
	public PlayerMovement PM;
	public CollisionIdentifierScript cIF;
	//Constructor to set default values
	public Collider childCollider;
	public DeathndGoal(){
		this.goal = false;
		this.death = false;
	}
	void Start () {
		//getDeathCount from LevelManager
		PM = this.GetComponent<PlayerMovement> ();
		cameraFollow = Camera.main.GetComponent<CameraFollow> ();
		deathCount = PlayerPrefs.GetInt ("Death");		
		time = this.GetComponent<Timer> ();
		speedOMeter = GameObject.FindWithTag ("SpeedOMeter");
		speedbar = speedOMeter.GetComponent<EnergyBar>();
		partSizeZ = SizeZ/4f;
		partSizeX = (SizeZ/3f)/2f;
	}

	void OnTriggerEnter(Collider other){
		Debug.Log (other.tag);
		if (other.tag == "DeathField" && !this.goal && !this.death){
			this.Death();
		}
		if (other.tag == "Goal" && !this.death){
			this.Goal();
		}
		if (other.tag == "BK_Burning" && !this.death){
			this.FlameThrower(other.transform.position);
			this.death = true;
		}
		if (other.tag == "BK_Sticky" && !this.death){
			this.Sticky(other.transform.position);
		}
		if (other.tag == "BK_Boost" && !this.death){
			this.Boost();
			Debug.Log ("boosster called");
		}
		if (other.tag == "Key"){
			keyActimationTriggers();
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "BK_Sticky"){
			GameObject[] t=GameObject.FindGameObjectsWithTag("Turret");
			foreach(GameObject turret in t){
				Destroy(turret);
			}
		}
	}
	void OnCollisionEnter(Collision collision){
		if(collision.collider==childCollider){
			Debug.Log("collsiosn on child has occured");
		}else{
			Debug.Log ("Collision has occured in DG");
		}
		foreach (ContactPoint c in collision.contacts)
		{
			if(c.thisCollider.name == "ColliderIdentifier"){
				if(speedbar.valueCurrent > CollisionSpeed && !this.death && !this.goal){
					cameraFollow.exploded=true;
					Explode();
				}
			}
		}
		/*ContactPoint contact = collision.contacts[0];
		if (contact.normal.y < 0.9f){
			Debug.Log ("speedbar.valueCurrent = "+speedbar.valueCurrent);
		}
		
		if (contact.normal.y < 0.9f && ){

		}*/
		//Debug.Log("Points colliding: " + collision.contacts.Length);
		//Debug.Log("First normal of the point that collide: " + collision.contacts[0].normal);
	}
	void keyActimationTriggers(){
		foundKey = 1;
		PlayerPrefs.SetInt("FoundKey",foundKey);
		normalKeyAnimation.Stop ();
		KeyTriggeranimation.Play();
		Destroy (Key);
		if (!InfoBox.enabled) {
			InfoBox.initialFunction=true;
			InfoBox.enabled = true;
		}
	}
	void Goal(){
		goal = true;
		cameraFollow.goal = true;
		FinalSetup ();
		Destroy (this.gameObject);
	}
	public void heatDeath(){
		Death ();
	}
	void Death(){
		death = true;
		cameraFollow.dead = true;
		FinalSetup ();
		Destroy (this.gameObject);
	}

	void FinalSetup(){
		if (goal) {
			/*set for levelManager*/
			PlayerPrefs.DeleteAll ();
			PlayerPrefs.SetInt("isPlayerPrefSet", 1);
			PlayerPrefs.SetInt("Stage",stage);
			PlayerPrefs.SetInt("Level",level);
			PlayerPrefs.SetInt("Completed",1);
			PlayerPrefs.SetInt("FoundKey",foundKey);
			PlayerPrefs.SetFloat("Time", time.timeTaken);
			PlayerPrefs.SetInt("stars",calculateStars());
			if(time.timeTaken<time.BestTime){
				PlayerPrefs.SetInt ("newBestScore", 1);			
			}else{
				PlayerPrefs.SetInt ("newBestScore", 0);		
			}
			/*set for FinalStatscalculation*/
			PlayerPrefs.SetInt("timeMinutes",time.minutes);
			PlayerPrefs.SetInt("timeSeconds",time.seconds);
			PlayerPrefs.SetFloat("timeFractions",time.fraction);
			PlayerPrefs.SetFloat("timeTaken",time.timeTaken);
			Debug.Log (goal);
			Debug.Log(PlayerPrefs.GetFloat ("Time")+"total time from Death and Goal");
			PlayerPrefs.Save();
		}
		if (death) {

			PlayerPrefs.DeleteAll ();
			PlayerPrefs.SetInt ("isPlayerPrefSet", 2);
			PlayerPrefs.SetInt("Stage",stage);
			PlayerPrefs.SetInt("Level",level);
			deathCount=deathCount+1;
			PlayerPrefs.SetInt("Death",deathCount);
			PlayerPrefs.SetInt("FoundKey",foundKey);

			/*set for FinalStatscalculation*/
			PlayerPrefs.SetInt("timeMinutes",time.minutes);
			PlayerPrefs.SetInt("timeSeconds",time.seconds);
			PlayerPrefs.SetFloat("timeFractions",time.fraction);
			PlayerPrefs.SetFloat("timeTaken",time.timeTaken);
			PlayerPrefs.Save();
		}
	}
	int calculateStars(){
		int setStars = 0;
		int min = time.minutes;
		int sec = time.seconds;
		Debug.Log ("min "+min+"sec "+sec);
		if ((min <= star5.x) && (sec <= star5.y)) {
			Debug.Log ("Star 5 Minute");
			setStars = 5;
		} else if ((min  <= star4.x) && (sec > star5.y && sec <= star4.y)) {
			Debug.Log ("Star 4 Minute");
			setStars = 4;
		}else if ((min  <= star3.x) && (sec > star4.y && sec <= star3.y)) {
			Debug.Log ("Star 3 Minute");
			setStars = 3;
		}else if ((min  <= star2.x) && (sec > star3.y && sec <= star2.y)) {
			Debug.Log ("Star 2 Minute");
			setStars = 2;
		}else if (( min  <= star1.x) && (sec > star2.y && sec <= star1.y)) {
			Debug.Log ("Star 1 Minute");
			setStars = 1;
		}

		return setStars;

	}
	void Sticky(Vector3 pos){
				Debug.Log ("Stage 2");
				float halfLength=SizeZ/2f;
				float halfWidth=SizeX/2f;
				Instantiate(this.StickyTurret,new Vector3(pos.x+halfWidth-0.5f,pos.y+10f,pos.z-halfLength+0.5f),Quaternion.identity);
				Instantiate(this.StickyTurret,new Vector3(pos.x-halfWidth+0.5f,pos.y+10f,pos.z-halfLength+0.5f),Quaternion.identity);
				Instantiate(this.StickyTurret,new Vector3(pos.x-halfWidth+0.5f,pos.y+10f,pos.z+halfLength-0.5f),Quaternion.identity);
				Instantiate(this.StickyTurret,new Vector3(pos.x+halfWidth-0.5f,pos.y+10f,pos.z+halfLength-0.5f),Quaternion.identity);
	}

	void Boost(){
		if (!isBoosterOnSceen) {
			Instantiate(this.Boosters,new Vector3(this.transform.position.x,BoosterHeight,this.transform.position.z),Quaternion.identity);						
		}
	}

	public void Explode(){
		Vector3 pos = this.transform.position;
		pos.y = pos.y + 1.4620867f;
		pos.z = pos.z - 1.33206f;
		Instantiate(this.Blast, pos, this.transform.rotation);
		Death ();
	}

	void FlameThrower(Vector3 pos){
		if(this.transform.position.x>(pos.x+partSizeX)){
			Debug.Log ("invalid distance At Flame One");
			float div=((pos.x+partSizeX)-this.transform.position.x)/(this.transform.position.z-pos.z);
			float theta=Mathf.Rad2Deg*Mathf.Atan(div);
			Instantiate(this.FlameThrowerObject,new Vector3((pos.x+partSizeX),pos.y,(pos.z)),Quaternion.Euler(0,-(((theta<0)?(-90):(90))+theta),0));
			div=((pos.x-partSizeX)-this.transform.position.x)/(this.transform.position.z-(pos.z+0.253f));
			theta=Mathf.Rad2Deg*Mathf.Atan(div);
			Instantiate(this.FlameThrowerObject,new Vector3((pos.x-partSizeX),pos.y,(pos.z+0.253f)),Quaternion.Euler(0,-(((theta<0)?(-90):(90))+theta),0));

		}else if(this.transform.position.x<(pos.x-partSizeX)){
			Debug.Log ("invalid distance At Flame Two");	
			Debug.Log ("sx "+this.transform.position.x+" f1x "+(pos.x+partSizeX)+" f2x "+(pos.x-partSizeX));
			Debug.Log ("sz "+this.transform.position.z+" f1z "+(pos.z)+" f2z "+(pos.z+0.253f));

			float div=(this.transform.position.z-pos.z)/((pos.x+partSizeX)-this.transform.position.x);
			float theta=Mathf.Rad2Deg*Mathf.Atan(div);
			Instantiate(this.FlameThrowerObject,new Vector3((pos.x+partSizeX),pos.y,(pos.z)),Quaternion.Euler(0,theta,0));
			div=(this.transform.position.z-(pos.z+0.253f))/((pos.x-partSizeX)-this.transform.position.x);
			theta=Mathf.Rad2Deg*Mathf.Atan(div);
			Debug.Log("theta "+theta);
			Instantiate(this.FlameThrowerObject,new Vector3((pos.x-partSizeX),pos.y,(pos.z+0.253f)),Quaternion.Euler(0,theta,0));
		}else{
			Debug.Log ("Everything is correct");
			Instantiate(this.FlameThrowerObject,new Vector3((pos.x+partSizeX),pos.y,(pos.z)),Quaternion.Euler(0, Mathf.Rad2Deg*Mathf.Atan((this.transform.position.z-(pos.z))/((pos.x+partSizeX)-this.transform.position.x)), 0));
			Debug.Log ("opp "+(this.transform.position.z-(pos.z)));
			Debug.Log ("adj "+((pos.x+partSizeX)-this.transform.position.x));
			Debug.Log ("tan "+Mathf.Rad2Deg*Mathf.Atan((this.transform.position.z-(pos.z))/((pos.x+partSizeX)-this.transform.position.x)));
			Debug.Log ("sx "+this.transform.position.x+"sz "+this.transform.position.z);
			Debug.Log ("fx "+(pos.x-partSizeX)+"fz "+(pos.z+0.253f));
			Debug.Log ("fx pos "+pos);
			Instantiate(this.FlameThrowerObject,new Vector3((pos.x-partSizeX),pos.y,(pos.z+0.253f)),Quaternion.Euler(0, 180-Mathf.Rad2Deg*Mathf.Atan((this.transform.position.z-(pos.z+0.253f))/((pos.x+partSizeX)-this.transform.position.x)), 0));		
			}
		cameraFollow.lastPosition = cameraFollow.currentPos;
		cameraFollow.lastRotaion = cameraFollow.currentRot;
		cameraFollow.playerposition = this.transform.position;
		cameraFollow.movePosition = new Vector3 (cameraFollow.currentPos.x,cameraFollow.currentPos.y+4.3f,cameraFollow.currentPos.z+3.8f);
		cameraFollow.dead = true;
		cameraFollow.FlameThrowerAct = true;
		Destroy(this.gameObject.GetComponent<Rigidbody>());	
		StartCoroutine (waitForDeath());
	}	
	IEnumerator waitForDeath(){
		yield return new WaitForSeconds (3f);
		Instantiate (ExplosionBurner, new Vector3(this.transform.position.x+0.056f,
		                                          this.transform.position.y+0.4f,
		                                          this.transform.position.z-1f), this.transform.rotation);
		Instantiate (fireBurner, new Vector3(this.transform.position.x+0.087f,
		                                          this.transform.position.y,
		                                          this.transform.position.z-1f), this.transform.rotation);

		Death ();
	}
	float calculateAngle(){
		float AngleOfDeflection = 0f;
		return AngleOfDeflection;
	}
}
