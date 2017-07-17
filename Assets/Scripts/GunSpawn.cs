using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawn : MonoBehaviour {

	public static GunSpawn instance ; 
	public GameObject[] spawnPoints;
	public GameObject currentPoint;
	int index;
	public GameObject gun;
	private Vector3 pos;


	public void Start() {
		GunSpawn instance = new GunSpawn ();
		Invoke ("Respawning", 0f);

	}
	public void Spawn () {
		gun.transform.position = currentPoint.transform.position;
		gun.SetActive (true);

	}
	public void Respawning () {
		StartCoroutine (Spawning (1f));

	}
	public IEnumerator Spawning (float sec) {
		gun.SetActive (false);
		index = Random.Range (0, spawnPoints.Length);
		currentPoint = spawnPoints [index];
		print (currentPoint.name);

		Invoke ("Spawn", 2.5f);
		yield return new WaitForSeconds (sec);

	}

}
