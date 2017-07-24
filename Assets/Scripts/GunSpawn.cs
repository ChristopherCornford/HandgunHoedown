using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawn : MonoBehaviour {

	public static GunSpawn instance ; 
	public GameObject[] spawnPoints;
	public GameObject currentPoint;
	int index;
	public GameObject gun;
	public GameObject gunPrefab;
	private Vector3 pos;


	public void Spawn () {
		gun = Instantiate (gunPrefab, currentPoint.transform.position, currentPoint.transform.rotation) as GameObject;

	}

	public void Respawning () {
		GunSpawn instance = new GunSpawn ();
		if (gun.activeInHierarchy == true) {
			Destroy (gun);
		}
		StartCoroutine (Spawning (1f));

	}
	public IEnumerator Spawning (float sec) {
		index = Random.Range (0, spawnPoints.Length);
		currentPoint = spawnPoints [index];
		print (currentPoint.name);

		Invoke ("Spawn", 2.5f);
		yield return new WaitForSeconds (sec);

	}
}
