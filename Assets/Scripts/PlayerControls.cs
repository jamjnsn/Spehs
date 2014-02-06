using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipMotor))]
public class PlayerControls : ShipController {

	void Update () {
		Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		motor.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		motor.Move(direction);	
		
		Vector3 distance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		if(Input.GetAxis("Fire1") != 0f)
			motor.Fire(distance.ToVector2().normalized);
	}
}
