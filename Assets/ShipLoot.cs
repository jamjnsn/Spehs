using UnityEngine;
using System.Collections;

public class ShipLoot : MonoBehaviour {
	public GameObject PowerUpContainer;
	public float Chance;
	
	void ShipDestroyed()
	{
		if(PowerUpContainer == null || PowerUpContainer.GetComponent<PowerupContainer>() == null) return;
		if(Random.Range(0, 100) < Chance)
		{
			GameObject container = Instantiate (PowerUpContainer) as GameObject;
			container.transform.position = transform.position;
		}
	}
}
