using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipMotor))]
public class ShipController : MonoBehaviour {
	protected ShipMotor motor;
	
	// Use this for initialization
	void Start () {
		motor = GetComponent<ShipMotor>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
