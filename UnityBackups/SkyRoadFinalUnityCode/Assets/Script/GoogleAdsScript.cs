using UnityEngine;
using System;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class GoogleAdsScript : MonoBehaviour {

	private BannerView mainScreenBannerView;
	private BannerView levelBannerView;
	private BannerView levelManagerBannerView;
	private BannerView bannerView;
	private InterstitialAd interstitial;
	private static string outputMessage = "";
	bool isOpen=false;
	void OnDestroy() {
		
		if(Application.loadedLevelName=="MainSCreen"){
			Debug.Log("main screen unload");
			if(mainScreenBannerView!=null){
				mainScreenBannerView.Hide();
				Debug.Log("Main Screen Hide");
			}
		}else if(Application.loadedLevelName=="LevelManager"){
			Debug.Log("levelmanager unload");
			if(levelBannerView!=null){
				levelBannerView.Hide();
				Debug.Log("Level Screen Hide");
			}
		}else{
			Debug.Log("other unload");
			if(levelManagerBannerView!=null){
				levelManagerBannerView.Hide();
				Debug.Log("Level Manager Screen Show");
			}
		}
	}
	// Use this for initialization
	void Start (){
		if(Application.loadedLevelName=="MainSCreen"){
			Debug.Log("Main screen adddddddddddddd");
			RequestBottomBanner();
		}else if(Application.loadedLevelName=="LevelManager"){
			Debug.Log("levelmanager adddddddd");
			RequestTopBanner();
		}else{
			Debug.Log("other adssssssssss");
			RequestBottomStandardBanner();
		}
		RequestInterstitial ();	
	}	
	void Update(){
		if(isOpen){
			if(Input.GetKey(KeyCode.Escape)){
				interstitial.Destroy();
				isOpen=false;
			}
		}
	}
	public static string OutputMessage{
		set { outputMessage = value; }
	}
	private void RequestBottomBanner(){
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-2660720400546892/1460738961";
		#elif UNITY_IPHONE
		string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Create a 320x50 banner at the top of the screen.
		mainScreenBannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
		// Register for ad events.
		mainScreenBannerView.AdLoaded += HandleAdLoaded;
		mainScreenBannerView.AdFailedToLoad += HandleAdFailedToLoad;
		mainScreenBannerView.AdOpened += HandleAdOpened;
		mainScreenBannerView.AdClosing += HandleAdClosing;
		mainScreenBannerView.AdClosed += HandleAdClosed;
		mainScreenBannerView.AdLeftApplication += HandleAdLeftApplication;
		// Load a banner ad.
		mainScreenBannerView.LoadAd(createAdRequest());
	}
	private void RequestBottomStandardBanner(){
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-2660720400546892/1460738961";
		#elif UNITY_IPHONE
		string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Create a 320x50 banner at the top of the screen.
		levelBannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
		// Register for ad events.
		levelBannerView.AdLoaded += HandleAdLoaded;
		levelBannerView.AdFailedToLoad += HandleAdFailedToLoad;
		levelBannerView.AdOpened += HandleAdOpened;
		levelBannerView.AdClosing += HandleAdClosing;
		levelBannerView.AdClosed += HandleAdClosed;
		levelBannerView.AdLeftApplication += HandleAdLeftApplication;
		// Load a banner ad.
		levelBannerView.LoadAd(createAdRequest());
	}
	private void RequestTopBanner(){
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-2660720400546892/1460738961";
		#elif UNITY_IPHONE
		string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Create a 320x50 banner at the top of the screen.
		levelBannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
		// Register for ad events.
		levelBannerView.AdLoaded += HandleAdLoaded;
		levelBannerView.AdFailedToLoad += HandleAdFailedToLoad;
		levelBannerView.AdOpened += HandleAdOpened;
		levelBannerView.AdClosing += HandleAdClosing;
		levelBannerView.AdClosed += HandleAdClosed;
		levelBannerView.AdLeftApplication += HandleAdLeftApplication;
		// Load a banner ad.
		levelBannerView.LoadAd(createAdRequest());
	}

	private void RequestInterstitial(){
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-2660720400546892/5534024966";
		#elif UNITY_IPHONE
		string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Create an interstitial.
		interstitial = new InterstitialAd(adUnitId);
		// Register for ad events.
		interstitial.AdLoaded += HandleInterstitialLoaded;
		interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitial.AdOpened += HandleInterstitialOpened;
		interstitial.AdClosing += HandleInterstitialClosing;
		interstitial.AdClosed += HandleInterstitialClosed;
		interstitial.AdLeftApplication += HandleInterstitialLeftApplication;              
		// Load an interstitial ad.
		interstitial.LoadAd(createAdRequest());
	}
	
	private AdRequest createAdRequest(){
		return new AdRequest.Builder ()
				.AddKeyword("game")
				.Build();
	}
	
	private void ShowInterstitial(){
		if (interstitial.IsLoaded()){
			interstitial.Show();
		}
		else{
			print("Interstitial is not ready yet.");
		}
	}
	public void showFullScreen(){
		if (interstitial.IsLoaded()){
			interstitial.Show();
		}
	}
	#region Banner callback handlers
	
	public void HandleAdLoaded(object sender, EventArgs args){
		print("HandleAdLoaded event received.");
	}
	
	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args){
		print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}
	
	public void HandleAdOpened(object sender, EventArgs args){
		print("HandleAdOpened event received");
	}
	
	void HandleAdClosing(object sender, EventArgs args){
		print("HandleAdClosing event received");
	}
	
	public void HandleAdClosed(object sender, EventArgs args){
		print("HandleAdClosed event received");
	}
	
	public void HandleAdLeftApplication(object sender, EventArgs args){
		print("HandleAdLeftApplication event received");
	}	
	#endregion
	
	#region Interstitial callback handlers	
	public void HandleInterstitialLoaded(object sender, EventArgs args){
		print("HandleInterstitialLoaded event received.");
	}
	
	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args){
		print("HandleInterstitialFailedToLoad event received with message: " + args.Message);

	}
	
	public void HandleInterstitialOpened(object sender, EventArgs args){
		print("HandleInterstitialOpened event received");
		isOpen = true;
	}
	
	void HandleInterstitialClosing(object sender, EventArgs args){
		print("HandleInterstitialClosing event received");
	}
	
	public void HandleInterstitialClosed(object sender, EventArgs args){
		print("HandleInterstitialClosed event received");
		RequestInterstitial ();
	}
	
	public void HandleInterstitialLeftApplication(object sender, EventArgs args){
		print("HandleInterstitialLeftApplication event received");
	}
	
	#endregion
}
