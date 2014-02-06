using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipMotor))]
public class PhantomAI : ShipController {
	public GameObject Player;
	public float FollowDistance = 10f;
	
	void Update () {
		if(Player == null) return;
		
		motor.LookAt(Player.transform.position);
		
		Vector2 distance = Player.transform.position - transform.position;
		float sensitivity = 0.1f;
		if(distance.magnitude > FollowDistance - sensitivity)
			motor.Move(distance.ToVector2().normalized);
		else if(distance.magnitude < FollowDistance + sensitivity)
			motor.Move (distance.ToVector2().normalized * -1f);
		else motor.SetFlames(false);
		
		motor.Fire(distance.normalized);
	}
}
