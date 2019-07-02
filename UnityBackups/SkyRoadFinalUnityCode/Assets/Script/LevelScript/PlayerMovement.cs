using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float gravity=-9.81f;
	bool isGrounded;
	public Vector3 shipvelo;
	public float shipmagno;
	public float MaxHorSpeed;
	public float forSpeedMul;
	public float horizantalClamp;
	public float VerticalClamp;
	public float jumpSpeed=0.7f;
	GameObject speedOMeter;
	GameObject heatMeter;
	GameObject OxygenMeter;
	private EnergyBar hMeter;
	private EnergyBar OMeter;
	private EnergyBar speedbar;
	public float fuelusage=0f;
	public UILabel SpeedIndicator;
	public UILabel OxygenPercentage;
	public TweenRotation OxyRotate;
	public TweenScale OxyScale;
	public AudioClip JumpAudio;
	int HvalueMin;
	int HvalueMax;
	int HvalueCurrent;
	bool audioPlayed;
	public GameAudioCOntrollerLevel gA;
	public bool isBoosterOnSceen=false;
	bool isOnOxygen=false;
	float tempforspeed;
	float tempvClamp;
	float tempHorspeed;
	float tempHClamp;
	bool isOgVelocitySaved=false;
	Vector3 OgVelocity;
	public bool isSlippery=false;
	public CameraFollow cf;
	public CollisionIdentifierScript cIF;
	public UISlider verticleSlider;
	float verticleVal=0f;
	int horiValue = 0;
	bool jumpedButton=false;
	DeathndGoal dng;
	//public float raySize=0.1f;
	 
	void Start(){
		dng= this.gameObject.GetComponent<DeathndGoal>();
		Physics.gravity = new Vector3 (0, gravity, 0);
		speedOMeter = GameObject.FindWithTag ("SpeedOMeter");
		heatMeter = GameObject.FindWithTag ("HealthMeter");
		OxygenMeter = GameObject.FindWithTag ("OxygenMeter");
		speedbar = speedOMeter.GetComponent<EnergyBar>();
		hMeter = heatMeter.GetComponent<EnergyBar>();
		OMeter = OxygenMeter.GetComponent<EnergyBar>();
		hMeter.valueMin = 0;
		hMeter.valueMax = 12;
		hMeter.valueCurrent = 12;
		HvalueMin = 0;
		HvalueMax = 1200;
		HvalueCurrent = 1200;
		speedbar.valueMin = 0;
		speedbar.valueMax = (int)VerticalClamp;
		speedbar.valueCurrent = 0;
		OMeter.valueMin = 0;
		OMeter.valueMax = 30000;
		OMeter.valueCurrent = 30000;
		cf.OriginalPlayer = this.gameObject;
		cf.PlayerCreated = true;
	}
	void OnTriggerEnter(Collider other){
		if (other.tag == "BK_Oxygen") {
			Debug.Log ("Found");
			OxyScale.PlayForward();
			isOnOxygen=true;
		}

		if (other.tag == "BK_Sticky"){
			tempforspeed=forSpeedMul;
			tempvClamp=VerticalClamp;
			tempHorspeed=MaxHorSpeed;
			tempHClamp=horizantalClamp;
			forSpeedMul=1f;
			VerticalClamp=1f;
			MaxHorSpeed=1f;
			horizantalClamp=1f;
		}
		if (other.tag == "BK_Slippery"){
			isSlippery=true;
		}
	}
	void OnTriggerExit(Collider other){
		if (other.tag == "BK_Oxygen") {
			OxyRotate.enabled=false;
			OxyScale.PlayReverse();
			isOnOxygen=false;
		}
		if (other.tag == "BK_Sticky"){
			forSpeedMul=tempforspeed;
			VerticalClamp=tempvClamp;
			MaxHorSpeed=tempHorspeed;
			horizantalClamp=tempHClamp;		
		}
		if (other.tag == "BK_Slippery"){
			isSlippery=false;
		}
	}
	void Update(){
		if (Time.timeScale == 0) {
			return;
		}
		if (this.GetComponent<Rigidbody> ()) {
			fuelusage = Mathf.Floor (30000 / 500) * rigidbody.velocity.z /400 ;
			speedbar.valueCurrent = (int)rigidbody.velocity.z;
			if (isOnOxygen) {
				OMeter.valueCurrent=OMeter.valueMax;
			}else if (speedbar.valueCurrent == 0) {
				OMeter.valueCurrent = OMeter.valueCurrent - 1;
			} else {
				OMeter.valueCurrent = OMeter.valueCurrent - (int)fuelusage;
			}
			//Debug.Log (HvalueCurrent);
			if (speedbar.valueCurrent > 25 && speedbar.valueCurrent < 37) {
				HvalueCurrent = HvalueCurrent - 1;
				if(hMeter.valueCurrent==0){
					dng.heatDeath();
				}
				if((HvalueCurrent%100)==0){
					hMeter.valueCurrent = hMeter.valueCurrent - 1;
					print("Dead 2");
				}
			} else if (speedbar.valueCurrent > 38) {
				HvalueCurrent = HvalueCurrent - 2;
				if(hMeter.valueCurrent==0){
					dng.heatDeath();
				}
				if((HvalueCurrent%100)==0){
					hMeter.valueCurrent = hMeter.valueCurrent - 1;
					print("Dead 3");
				}
			} else {
				if(hMeter.valueCurrent==0){
					dng.heatDeath();
				}
				if(hMeter.valueCurrent<hMeter.valueMax){
					HvalueCurrent = HvalueCurrent + 1;
					if((HvalueCurrent%100)==0){
						hMeter.valueCurrent=hMeter.valueCurrent+1;
					}
				}
			}	
		}


		Debug.DrawRay (transform.position, -Vector3.up);
		if(Physics.Raycast (this.transform.position, -Vector3.up, 1f)){
			//Debug.DrawRay(transform.position, -Vector3.up * raySize, Color.green);
			isGrounded=true;
			/*if(isGrounded && audioPlayed){
				Debug.Log ("audioPlayed success");
				audioPlayed=false;
			}*/
		}
		else{
			isGrounded=false;
		}
		SpeedIndicator.text = speedbar.valueCurrent +" "+"KM/H";
		float current=OMeter.valueCurrent;
		float max=OMeter.valueMax;
		float percentage = (current / max);
		OxygenPercentage.text = string.Format("{0:00 %}",percentage);
	}

	void FixedUpdate ()
	{
		if (this.GetComponent<Rigidbody> ()) {
			shipvelo = rigidbody.velocity;
			shipmagno = rigidbody.velocity.magnitude;
			verticleVal=verticleSlider.value * 2f - 1f;
			if (rigidbody.velocity.z < 0) {
				Vector3 temp = rigidbody.velocity;
				temp.z=0;
				rigidbody.velocity=temp;	
			}
			rigidbody.AddForce (Vector3.forward * verticleVal * forSpeedMul);
			//rigidbody.AddForce (Vector3.forward * Input.GetAxis("Vertical") * forSpeedMul);
			if(isBoosterOnSceen){
				if(!isOgVelocitySaved){
					OgVelocity= rigidbody.velocity;
					isOgVelocitySaved=true;
				}
				Vector3 velocity  = rigidbody.velocity;
				velocity.x = Mathf.Clamp(horiValue * MaxHorSpeed, -horizantalClamp, horizantalClamp);
				//velocity.x = Mathf.Clamp(Input.GetAxisRaw("Horizontal") * MaxHorSpeed, -horizantalClamp, horizantalClamp);
				velocity.z =VerticalClamp;
				rigidbody.velocity = velocity;
			}else if(isSlippery){
				if(!isOgVelocitySaved){
					OgVelocity = rigidbody.velocity;
					isOgVelocitySaved=true;
				}
				Vector3 velocity  = rigidbody.velocity;
				velocity.x = Mathf.Clamp(Random.Range(1,-1) *4* MaxHorSpeed, -4*horizantalClamp, 4*horizantalClamp);
				velocity.z = Mathf.Clamp (rigidbody.velocity.z, -VerticalClamp, VerticalClamp);
				rigidbody.velocity = velocity;
			}else{
				Vector3 velocity  = rigidbody.velocity;
				if(isOgVelocitySaved){
					Destroy(GameObject.FindGameObjectWithTag("Booster"));		
					rigidbody.velocity=OgVelocity;
					isOgVelocitySaved=false;
				}

				velocity.x = Mathf.Clamp(horiValue * MaxHorSpeed, -horizantalClamp, horizantalClamp);
				//velocity.x = Mathf.Clamp(Input.GetAxisRaw("Horizontal") * MaxHorSpeed, -horizantalClamp, horizantalClamp);
				velocity.z = Mathf.Clamp (rigidbody.velocity.z, -VerticalClamp, VerticalClamp);
				rigidbody.velocity = velocity;
			}
			/*Vector3 velocity  = rigidbody.velocity;
			velocity.x = Mathf.Clamp(Input.GetAxisRaw("Horizontal") * MaxHorSpeed, -horizantalClamp, horizantalClamp);
			velocity.z = Mathf.Clamp (rigidbody.velocity.z, -VerticalClamp, VerticalClamp);
			rigidbody.velocity = velocity;
			*/
			if (jumpedButton && Physics.Raycast(this.transform.position, -Vector3.up, 0.5f))
			//if (Input.GetButton("Jump") && Physics.Raycast(this.transform.position, -Vector3.up, 0.5f))
			{	
				rigidbody.AddForce(Vector3.up *jumpSpeed,ForceMode.Impulse);
			}
		}	
	}
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Ground" ){
			audio.clip=gA.FXClips[0];
			audio.Play();
			Debug.Log ("Am Grounded");
		}
	}

	public void updateValueHorileft(){
		horiValue = -1;
		Debug.Log ("left callled");
	}
	public void updateValueHoriRight(){
		horiValue = 1;
		Debug.Log ("right callled");
	}
	public void removeValueHori(){
		if (horiValue != 0) {
			horiValue = 0;		
		}else{
			horiValue = 0;
		}
		Debug.Log ("remove calledx");
	}
	public void updateJumpButton(){
		jumpedButton = true;
	}
	public void removeJump(){
		jumpedButton = false;
	}
}

