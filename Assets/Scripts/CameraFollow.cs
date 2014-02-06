using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform Target;
	public float Speed = 1f;
	public bool X = true, Y = true, Z = false;
	public Vector3 Distance;

	// Update is called once per frame
	void Update () {
		if(Target != null)
		{
			Vector3 destination = transform.position;
			if(X) destination.x = Target.position.x;
			if(Y) destination.y = Target.position.y;
			if(Z) destination.z = Target.position.z;
			destination += Distance;
			transform.position = Vector3.Lerp(transform.position, destination, Speed * Time.deltaTime);
		}
	}
}
