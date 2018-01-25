using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipMotor))]
public class PlayerControls : ShipController {
	public int PlayerIndex = 0;
	public int Score = 0;
	public GameObject NoScopeBullet;
	
	bool rotating;
	Vector2 startVector;
	float rotGestureWidth;
	float rotAngleMinimum;

	void Update () {
		if(PlayerIndex == 0) return;
		
		if(Application.platform == RuntimePlatform.Android) UpdateAndroidControls();
		else UpdatePCControls();
	}
	
	void UpdateAndroidControls()
	{
		foreach(Touch touch in Input.touches)
		{
			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
			{
				startVector = touch.position;
				//rotating = startVector.sqrMagnitude > rotGestureWidth * rotGestureWidth;
				transform.rotation = Input.gyro.attitude;
			
				if(touch.position.x < Screen.width / 2)
				{
					//motor.Move (touch.deltaPosition.normalized);
				} else if(touch.position.x > Screen.width / 2)
				{
					Vector2 currVector = touch.position;
					float angleOffset = Vector2.Angle(startVector, currVector);
					Vector3 LR = Vector3.Cross(startVector, currVector);
					transform.Rotate(0f,0f,angleOffset);
					
					/*if (angleOffset > rotAngleMinimum) {
						if (LR.z > 0) {
							// Anticlockwise turn equal to angleOffset.
						} else if (LR.z < 0) {
							// Clockwise turn equal to angleOffset.
						}
					}*/
				}
			}
		}
	}

	void UpdatePCControls ()
	{
		Vector2 direction = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		motor.LookAt (Camera.main.ScreenToWorldPoint (Input.mousePosition));
		motor.Move (direction);
		Vector3 distance = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		if (Input.GetAxis ("Fire1") != 0f)
			motor.Fire (distance.ToVector2 ().normalized);
	}
	
	void HasExploded()
	{
		GetComponent<SpriteRenderer>().enabled = false;
		GameObject.Find("Scene Fader").GetComponent<SceneChanger>().StartSceneChange("Results");
	}
}
