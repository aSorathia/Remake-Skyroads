using UnityEngine;
using System.Collections;

public class ZoomEffects : MonoBehaviour {

	public EnergyBar SpeedOmeter;
	public TweenPosition speedOxygenmeterPos;
	public TweenPosition TimerPos;
	public TweenPosition HeatMeterPos;
	public TweenPosition OverlayPos;
	public TweenPosition LevelStatsPos;
	public TweenScale speedOxygenMeterScale;
	public TweenScale TimerScale;
	public TweenScale HeatMeterScale;
	public TweenColor speedOxygenMeterColor;
	public TweenColor TimerColor;
	public TweenColor HeatMeterColor;
	public TweenColor OverlayColor;
	public UISprite OverlaySprite;
	public GameObject CameraObject;
	public float Shakingfactor=0f;
	public GameObject Overlay;
	public int maxSpeed;
	public int zoomSpeed;

	bool playedOnceForward;
	bool playedOnceReverse;
	bool resetSetting = false;
	// Use this for initialization
	void Start () {

		maxSpeed = SpeedOmeter.valueMax;
		playedOnceForward = false;
		playedOnceReverse = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(PlayerPrefs.HasKey("isPlayerPrefSet") && !resetSetting){
			speedOxygenmeterPos.PlayReverse();
			TimerPos.PlayReverse();
			HeatMeterPos.PlayReverse();
			speedOxygenMeterScale.PlayReverse();
			TimerScale.PlayReverse();
			HeatMeterScale.PlayReverse();
			speedOxygenMeterColor.PlayReverse();
			TimerColor.PlayReverse();
			HeatMeterColor.PlayReverse();
			OverlayColor.enabled=false;
			OverlaySprite.color=new Color(255f,255f,255f);
			iTween.StopByName("shaker");
			OverlayPos.delay=3f;
			EventDelegate.Set(OverlayPos.onFinished,TweenStats);
			OverlayPos.PlayReverse();
		}

		if((SpeedOmeter.valueCurrent>=zoomSpeed)&&!(playedOnceForward) && Overlay.activeSelf){
			speedOxygenmeterPos.PlayForward();
			TimerPos.PlayForward();
			HeatMeterPos.PlayForward();
			speedOxygenMeterScale.PlayForward();
			TimerScale.PlayForward();
			HeatMeterScale.PlayForward();
			speedOxygenMeterColor.PlayForward();
			TimerColor.PlayForward();
			HeatMeterColor.PlayForward();
			OverlayColor.enabled=true;
			iTween.ShakePosition(CameraObject,iTween.Hash("name","shaker","x",Shakingfactor,"y",Shakingfactor ,"time",.1f,"looptype","loop"));
			playedOnceForward=true;
			playedOnceReverse=false;
		}
		if((SpeedOmeter.valueCurrent<=zoomSpeed)&& playedOnceForward && !playedOnceReverse){
			speedOxygenmeterPos.PlayReverse();
			TimerPos.PlayReverse();
			HeatMeterPos.PlayReverse();
			speedOxygenMeterScale.PlayReverse();
			TimerScale.PlayReverse();
			HeatMeterScale.PlayReverse();
			speedOxygenMeterColor.PlayReverse();
			TimerColor.PlayReverse();
			HeatMeterColor.PlayReverse();
			OverlayColor.enabled=false;
			OverlaySprite.color=new Color(255f,255f,255f);
			iTween.StopByName("shaker");
			playedOnceForward=false;
			playedOnceReverse=true;
		}
	}
	public void TweenStats(){
		LevelStatsPos.Play();
	}

}
