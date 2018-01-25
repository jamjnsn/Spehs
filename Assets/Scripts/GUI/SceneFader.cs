using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
public class SceneFader : MonoBehaviour 
{
	bool fadingIn = false, fadingOut = false;
	public float FadeSpeed = 1.5f;
	
	public bool FadedIn {
		get {
			return guiTexture.color == Color.clear;
		}
	}
	
	public bool FadedOut {
		get {
			return guiTexture.color == Color.black;
		}
	}
	
	void Awake()
	{
		fadingIn = true;
		guiTexture.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
	}
	
	void Update()
	{
		if(fadingIn)
			UpdateFadeIn ();
		else if(fadingOut)
			UpdateFadeOut ();
	}
	
	void UpdateFadeIn()
	{
		guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, FadeSpeed * Time.deltaTime);
		
		if(guiTexture.color.a <= 0.05f)
		{
			guiTexture.color = Color.clear;
			guiTexture.enabled = false;
			fadingIn = false;
		}
	}
	
	void UpdateFadeOut()
	{
		guiTexture.enabled = true;
		guiTexture.color = Color.Lerp(guiTexture.color, Color.black, FadeSpeed * Time.deltaTime);
		
		if(guiTexture.color.a >= 0.95f)
		{
			guiTexture.color = Color.black;
			fadingOut = false;
		}
	}
	
	public void FadeIn()
	{
		fadingIn = true;
	}
	
	public void FadeOut()
	{
		fadingOut = true;
	}
}
