using UnityEngine;
using System.Collections;
public class backgroundFit:MonoBehaviour {
	public GameObject theSprite;
	public Camera theCamera;
	public int fitToScreenWidth;
	public int fitToScreenHeight;

	void Start()
	{        
		SpriteRenderer sr = theSprite.GetComponent<SpriteRenderer>();
		
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
