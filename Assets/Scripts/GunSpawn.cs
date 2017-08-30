using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Greyman;

public class GunSpawn : MonoBehaviour {
	private GameManager GameManager;
	public static GunSpawn instance ; 
	public GameObject[] spawnPoints;
	public GameObject currentPoint;
	private GameObject nonpoint;
	public GameObject gun;
	public GameObject gunPrefab;
	private Vector3 pos;

	[Range(0, 2)]
	public int numGuns;

	[Header("Indicator Management")]
	public GameObject OffScreenIndicatorObj;
	private OffScreenIndicator OffScreenIndicator;

	void Start(){
		GameManager = GameObject.Find("/Managers/GameManager").GetComponent<GameManager>();
		OffScreenIndicator = OffScreenIndicatorObj.GetComponent<OffScreenIndicator>();
	}
		
	public IEnumerator SpawnGun (float delay) {
		yield return new WaitForSeconds(delay);
		switch (numGuns) {
		case 0:
			yield return new WaitForSeconds (delay);
			currentPoint = spawnPoints [Random.Range (0, spawnPoints.Length)];
			Debug.Log ("Current Point is : " + currentPoint.name);
			if (currentPoint != nonpoint) {
				gun = Instantiate (gunPrefab, currentPoint.transform.position, currentPoint.transform.rotation) as GameObject;
				gun.GetComponent<GunPickupDestroyer>().myTransform = currentPoint.transform;
				numGuns++;
				Debug.Log ("Number of Guns in play: " + numGuns);
			} else {
				currentPoint = spawnPoints [Random.Range (0, spawnPoints.Length)];
				Debug.Log ("Current Point is : " + currentPoint.name);
				if (currentPoint != nonpoint) {
					gun = Instantiate (gunPrefab, currentPoint.transform.position, currentPoint.transform.rotation) as GameObject;
					gun.GetComponent<GunPickupDestroyer>().myTransform = currentPoint.transform;
					numGuns++;
					Debug.Log ("Number of Guns in play: " + numGuns);
				} else {
					currentPoint = spawnPoints [Random.Range (0, spawnPoints.Length)];
					Debug.Log ("Current Point is : " + currentPoint.name);
					if (currentPoint != nonpoint) {
						gun = Instantiate (gunPrefab, currentPoint.transform.position, currentPoint.transform.rotation) as GameObject;
						gun.GetComponent<GunPickupDestroyer>().myTransform = currentPoint.transform;
						numGuns++;
						Debug.Log ("Number of Guns in play: " + numGuns);
					} else {
						currentPoint = spawnPoints [Random.Range (0, spawnPoints.Length)];
						Debug.Log ("Current Point is : " + currentPoint.name);
						if (currentPoint != nonpoint) {
							gun = Instantiate (gunPrefab, currentPoint.transform.position, currentPoint.transform.rotation) as GameObject;
							gun.GetComponent<GunPickupDestroyer>().myTransform = currentPoint.transform;
							numGuns++;
							Debug.Log ("Number of Guns in play: " + numGuns);
						} else {
							currentPoint = spawnPoints [Random.Range (0, spawnPoints.Length)];
							Debug.Log ("Current Point is : " + currentPoint.name);
							if (currentPoint != nonpoint) {
								gun = Instantiate (gunPrefab, currentPoint.transform.position, currentPoint.transform.rotation) as GameObject;
								gun.GetComponent<GunPickupDestroyer>().myTransform = currentPoint.transform;
								numGuns++;
								Debug.Log ("Number of Guns in play: " + numGuns);
							}
						}
					}
				}
			}
			nonpoint = currentPoint;
			Debug.Log ("Nonpoint is : " + nonpoint.name);
			GameManager.currentGunSpawn = StartCoroutine ("SpawnGun", delay);
			OffScreenIndicator.AddIndicator(currentPoint.transform, 0);
			break;

		case 1:
			yield return new WaitForSeconds (delay);
			currentPoint = spawnPoints [Random.Range (0, spawnPoints.Length)];
			Debug.Log ("Current Point is : " + currentPoint.name);
			if (currentPoint != nonpoint) {
				gun = Instantiate (gunPrefab, currentPoint.transform.position, currentPoint.transform.rotation) as GameObject;
				gun.GetComponent<GunPickupDestroyer>().myTransform = currentPoint.transform;
				numGuns++;
				Debug.Log ("Number of Guns in play: " + numGuns);
			} else {
				currentPoint = spawnPoints [Random.Range (0, spawnPoints.Length)];
				Debug.Log ("Current Point is : " + currentPoint.name);
				if (currentPoint != nonpoint) {
					gun = Instantiate (gunPrefab, currentPoint.transform.position, currentPoint.transform.rotation) as GameObject;
					gun.GetComponent<GunPickupDestroyer>().myTransform = currentPoint.transform;
					numGuns++;
					Debug.Log ("Number of Guns in play: " + numGuns);
				} else {
					currentPoint = spawnPoints [Random.Range (0, spawnPoints.Length)];
					Debug.Log ("Current Point is : " + currentPoint.name);
					if (currentPoint != nonpoint) {
						gun = Instantiate (gunPrefab, currentPoint.transform.position, currentPoint.transform.rotation) as GameObject;
						gun.GetComponent<GunPickupDestroyer>().myTransform = currentPoint.transform;
						numGuns++;
						Debug.Log ("Number of Guns in play: " + numGuns);
					} else {
						currentPoint = spawnPoints [Random.Range (0, spawnPoints.Length)];
						Debug.Log ("Current Point is : " + currentPoint.name);
						if (currentPoint != nonpoint) {
							gun = Instantiate (gunPrefab, currentPoint.transform.position, currentPoint.transform.rotation) as GameObject;
							gun.GetComponent<GunPickupDestroyer>().myTransform = currentPoint.transform;
							numGuns++;
							Debug.Log ("Number of Guns in play: " + numGuns);
						} else {
							currentPoint = spawnPoints [Random.Range (0, spawnPoints.Length)];
							Debug.Log ("Current Point is : " + currentPoint.name);
							if (currentPoint != nonpoint) {
								gun = Instantiate (gunPrefab, currentPoint.transform.position, currentPoint.transform.rotation) as GameObject;
								gun.GetComponent<GunPickupDestroyer>().myTransform = currentPoint.transform;
								numGuns++;
								Debug.Log ("Number of Guns in play: " + numGuns);
							}
						}
					}
				}
			}
			nonpoint = currentPoint;
			Debug.Log ("Nonpoint is : " + nonpoint.name);
			GameManager.currentGunSpawn = StartCoroutine ("SpawnGun", delay);
			OffScreenIndicator.AddIndicator(currentPoint.transform, 0);
			break;

		case 2:

			break;
		}

	}

	public void RemoveIndicator(Transform myTransform){
		OffScreenIndicator.RemoveIndicator(myTransform);
	}
	public IEnumerator checkForGuns (float sec) {
		yield return new WaitForSeconds (0.1f);
	}
}
