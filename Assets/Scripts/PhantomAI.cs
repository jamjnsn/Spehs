using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ShipMotor))]
public class PhantomAI : ShipController {
	public float FollowDistance = 10f;
	
	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag(Tags.Player);
		if(!player.GetComponent<ShipMotor>().Destroyed && player != null)
		{
			motor.LookAt(player.transform.position);
			
			Vector2 distance = player.transform.position - transform.position;
			float sensitivity = 0.5f;
			if(distance.magnitude > FollowDistance + sensitivity)
				motor.Move(distance.ToVector2().normalized);
			else if(distance.magnitude < FollowDistance - sensitivity)
				motor.Move (distance.ToVector2().normalized * -1f);
			else motor.SetFlames(false);
			
			motor.Fire(distance.normalized);
		} else {
			motor.MoveForward ();
		}
	}
	
	void HasExploded()
	{
		List<GameObject> destroyQueue = new List<GameObject>();
		destroyQueue.Add (this.gameObject);
		foreach(Transform child in transform) destroyQueue.Add (child.gameObject);
		destroyQueue.ForEach(obj => Destroy (obj));
	}
}
