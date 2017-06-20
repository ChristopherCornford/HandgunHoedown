using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerManager {
	public Color playerColor;
	public Transform spawnPoint;
	[HideInInspector] public GameManager manager;
	[HideInInspector] public int playerNumber;
	[HideInInspector] public GameObject instance;
	[HideInInspector] public int wins;

	private PlayerMovement movement;
	private PlayerShooting shooting;

	public void Setup() {
		
		movement = instance.GetComponent<PlayerMovement> ();
		shooting = instance.GetComponent<PlayerShooting> ();




		movement.m_playerNumber = playerNumber;

		MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer> ();

		for ( int i = 0; i < renderers.Length; i++) {
			renderers[i].material.color = playerColor;
		}
	}

	/*public void DisableControls () {

		movement.enabled = false;
		shooting.enabled = false;
	}

	public void EnableControls () { 

		movement.enabled = true;
		shooting.enabled = true;
	}

	public void Reset () { 

		instance.transform.position = spawnPoint.position;
		instance.transform.rotation = spawnPoint.rotation;

		instance.SetActive (false);
		instance.SetActive (true);
	}*/
}