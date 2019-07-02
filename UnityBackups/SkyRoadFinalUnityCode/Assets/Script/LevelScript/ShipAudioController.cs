using UnityEngine;
using System.Collections;


public class ShipAudioController : MonoBehaviour {
	public Transform ThrustersourceVehicleStuff;
	public AudioClip[] FXClips;
	public AudioSource ThrustersSound;
	public AudioSource FXSoundSource;
	public GameObject Player;
	public EnergyBar SpeedOmeter;
	public bool dead;
	public bool goal;
	public ZoomEffects zoomEffects;
	public int minSpeed;
	public int maxSpeed;
	public int currentSpeed;
	public float minPitch;
	public float maxPitch;
	public float currentPitch;
	void Start(){

		dead = false;
		goal = false;
		Player = GameObject.FindWithTag ("Player");	
		minSpeed=zoomEffects.zoomSpeed;
		maxSpeed = SpeedOmeter.valueMax;
		minPitch = ThrustersSound.pitch;

	}

	void Update(){
		currentPitch=ThrustersSound.pitch;
		maxSpeed = SpeedOmeter.valueMax;
		if (!dead && Player) {
			currentSpeed = SpeedOmeter.valueCurrent;
			transform.position = Player.transform.position;
			ThrustersourceVehicleStuff.position=Player.transform.position;
			if(currentSpeed > minSpeed){			
				ThrustersSound.pitch=pitchCalculator();
			}
		}
		if (goal) {
			ThrustersSound.enabled=false;
			goal=false;
		}
		if (dead) {
			ThrustersSound.enabled=false;		
			dead=false;
		}

	}

	public void playFXClip(int id){
		audio.clip=FXClips[id];
		audio.Play();
	}
	public void playFXClip(float volume,int id){
		audio.clip=FXClips[id];
		audio.volume = volume;
		audio.Play();
	}
	float pitchCalculator(){
		float diffSpeed = maxSpeed - minSpeed;
		float clampedValue = maxSpeed - currentSpeed;
		float clampValueBin = 1-(clampedValue / diffSpeed);

		float diffPitch = maxPitch - minPitch;
		float temp = diffPitch * clampValueBin;
		float finalValue = temp + minPitch;
	    return (temp + minPitch);	
	}
}
