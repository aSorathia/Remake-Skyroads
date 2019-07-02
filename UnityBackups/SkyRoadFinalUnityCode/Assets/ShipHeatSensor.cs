using UnityEngine;
using System.Collections;

public class ShipHeatSensor : MonoBehaviour {

	GameObject heatMeter;
	private EnergyBar hMeter;
	int HvalueCurrent;

	GameObject speedOMeter;
	private EnergyBar speedbar;


	void Start () {
		heatMeter = GameObject.FindWithTag ("HealthMeter");
		hMeter = heatMeter.GetComponent<EnergyBar>();
		speedOMeter = GameObject.FindWithTag ("SpeedOMeter");
		speedbar = speedOMeter.GetComponent<EnergyBar>();
	}
	

	void Update () {
		if (speedbar.valueCurrent > 25 && speedbar.valueCurrent < 37) {
			HvalueCurrent = HvalueCurrent - 1;
			if((HvalueCurrent%100)==0){
				hMeter.valueCurrent = hMeter.valueCurrent - 1;
			}
		} else if (speedbar.valueCurrent > 38) {
			HvalueCurrent = HvalueCurrent - 2;
			if((HvalueCurrent%100)==0){
				hMeter.valueCurrent = hMeter.valueCurrent - 2;
			}
		} else {
			if(hMeter.valueCurrent<hMeter.valueMax){
				HvalueCurrent = HvalueCurrent + 1;
				if((HvalueCurrent%100)==0){
					hMeter.valueCurrent=hMeter.valueCurrent+1;
				}
			}
		}	
	}
}
