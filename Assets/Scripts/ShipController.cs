using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipMotor))]
public class ShipController : MonoBehaviour {
	protected ShipMotor motor;
	
	void Start () {
		motor = GetComponent<ShipMotor>();
	}
}
