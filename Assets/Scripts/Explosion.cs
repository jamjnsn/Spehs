using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	public GameObject ShipObject;

	void Update () {
		if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Explosion Done"))
		{
			Destroy (this.gameObject);
			ShipObject.SendMessage("HasExploded", SendMessageOptions.DontRequireReceiver);
		}
	}
}
