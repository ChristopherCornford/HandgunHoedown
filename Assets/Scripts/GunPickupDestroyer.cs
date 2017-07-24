using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickupDestroyer : MonoBehaviour {

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.tag == "Cowboy") {
			print ("Should be gone");
			Destroy (gameObject);
		}
	}
}
