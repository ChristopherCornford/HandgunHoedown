using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Greyman;
public class GunPickupDestroyer : MonoBehaviour {
	private GunSpawn GunSpawn;
	void Awake(){
		GunSpawn = GameObject.Find ("/Managers/GunSpawner").GetComponent<GunSpawn> ();
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.tag == "Cowboy") {
			GunSpawn.RemoveIndicator();
			Destroy (gameObject);
		}
	}
}
