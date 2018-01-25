using UnityEngine;
using System.Collections;

public class HealthTracker : MonoBehaviour {
	public Ship Ship;
	public float LerpSpeed = 1.0f;
	
	// Update is called once per frame
	void Update () {
		Rect pixelInset = guiTexture.pixelInset;
		if(Ship.Health == 0) pixelInset.width = -Screen.width;
		else if(Ship.Health == Ship.MaxHealth) pixelInset.width = 0;
		else pixelInset.width = -Screen.width + Mathf.RoundToInt(Screen.width / Ship.MaxHealth * Ship.Health);
		guiTexture.pixelInset = pixelInset;
	}
}
