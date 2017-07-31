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

	public IEnumerator SpawnGun (float delay) {
		yield return new WaitForSeconds(delay);
		currentPoint = spawnPoints[Random.Range (0, spawnPoints.Length)];
		Debug.Log(currentPoint.name);
		gun = Instantiate (gunPrefab, currentPoint.transform.position, currentPoint.transform.rotation) as GameObject;
		// yield return new WaitForSeconds (sec);
	}
}
