using UnityEngine;
using System.Collections;

public class BackgroundSetterLM : MonoBehaviour {
	public Sprite st1;
	public Sprite st2;
	public Sprite st3;
	public Sprite st4;
	public Sprite st5;
	public Sprite st6;
	public Sprite st7;
	public Sprite st8;
	public Sprite st9;
	public Sprite st10;
	public GameObject theSprite;
	public Camera theCamera;
	public int fitToScreenWidth;
	public int fitToScreenHeight;
	void Start () {
		backGrounfFit (st10);
	}
	public void setTexture1(){
		backGrounfFit (st1);
	}
	public void setTexture2(){

		backGrounfFit (st2);
	}
	public void setTexture3(){
	
		backGrounfFit (st3);
	}
	public void setTexture4(){

		backGrounfFit (st4);
	}
	public void setTexture5(){

		backGrounfFit (st5);
	}
	public void setTexture6(){

		backGrounfFit (st6);
	}
	public void setTexture7(){

		backGrounfFit (st7);
	}
	public void setTexture8(){

		backGrounfFit (st8);
	}
	public void setTexture9(){
	
		backGrounfFit (st9);
	}
	public void setTexture10(){

		backGrounfFit (st10);
	}
	void backGrounfFit(Sprite s){
		SpriteRenderer sr = theSprite.GetComponent<SpriteRenderer>();
		sr.sprite = s;
		theSprite.transform.localScale = new Vector3(1,1,1);
		
		float width = sr.sprite.bounds.size.x;
		float height = sr.sprite.bounds.size.y;
		print (width);
		print (height);
		float worldScreenHeight = (float)(theCamera.orthographicSize * 2.0);
		float worldScreenWidth = (float)(worldScreenHeight / Screen.height * Screen.width);
		print (worldScreenHeight);
		print (worldScreenWidth);
		if (fitToScreenWidth != 0)
		{
			Vector2 sizeX = new Vector2(worldScreenWidth / width / fitToScreenWidth,theSprite.transform.localScale.y);
			theSprite.transform.localScale = sizeX;
			print ("X "+sizeX);
		}
		
		if (fitToScreenHeight != 0)
		{
			Vector2 sizeY = new Vector2(theSprite.transform.localScale.x, worldScreenHeight / height / fitToScreenHeight);
			theSprite.transform.localScale = sizeY;
			print ("Y "+sizeY);
		}
	}
}
