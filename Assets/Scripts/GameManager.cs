using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public int numRoundsToWin = 5;
	public float startDelay = 3f;
	public float endDelay = 3f;
	public CameraControl cameraControl;
	public Text messageText;
	public PlayerManager[] player;
	public GameObject playerPrefab;
	public GameObject t_camera;

	private int roundNumber;
	private WaitForSeconds startWait;
	private WaitForSeconds endWait;
	private PlayerManager roundWinner;
	private PlayerManager gameWinner;

	// Use this for initialization
	void Start () {
		startWait = new WaitForSeconds (startDelay);
		endWait = new WaitForSeconds (endDelay);

		SpawnAllPlayers ();
		SetCameraTargets ();

		StartCoroutine (GameLoop ());
	}

	private void SpawnAllPlayers() {
		for (int i = 0; i < player.Length; i++) {

			player [i].instance = 
				Instantiate (playerPrefab, player [i].spawnPoint.position, player [i].spawnPoint.rotation) as GameObject;
			player [i].playerNumber = i + 1;
			player [i].Setup ();



		}
	}

	private void SetCameraTargets() {
		
		Transform[] targets = new Transform[player.Length];

		for (int i = 0; i < targets.Length; i++) {

			targets [i] = player[i].instance.transform;
		}
		t_camera.GetComponent<CameraControl>().m_Targets = targets;
	}
	private IEnumerator GameLoop() {

		yield return StartCoroutine (RoundStarting ());

		//yield return StartCoroutine (RoundPlaying ());

		//yield return StartCoroutine (RoundEnding());

	/*	if (gameWinner != null) {
			Application.LoadLevel (Application.loadedLevel);
		} else {
			StartCoroutine (GameLoop ());
		}*/

	}

	private IEnumerator RoundStarting() {
	//	ResetAllPlayers ();
	//	DisablePlayerControls ();

		cameraControl.SetStartPositionAndSize ();

		//roundNumber++;

		yield return startWait;
	}

/*	private IEnumerator RoundPlaying() {
		EnablePlayerControl ();

		while (!OnePlayerLeft ()) {
			yield return null;
		}
	}

	private IEnumerator RoundEnding() {
		DisablePlayerControls ();

		roundWinner = GetRoundWinner ();

		if (roundWinner != null) {
			roundWinner.wins++;
		}
		gameWinner = GetGameWinner ();

		yield return endWait;
	}

	private bool OnePlayerLeft () {

		int numPlayersLeft = 0;

		for (int i = 0; i < player.Length; i++) {

			if (player [i].instance.activeSelf) {
				return player [i];
			}
			return numPlayersLeft <= 1;
		}
	}
	private PlayerManager GetRoundWinnewr() {

		for (int i = 0; i < player.Length; i++) {
			if (player [i].instance.activeSelf)
				return player [i];
		}
		return null;
	}*/

	private PlayerManager GetGameWinnewr() {

		for (int i = 0; i < player.Length; i++) {
			if (player [i].wins == numRoundsToWin)
				return player [i];
		}
		return null;
	}
	
	/*private void ResetAllPlayers () {

			for (int i = 0; i < player.Length; i++) {
				player[i].Reset();
			}
	}

	private void EnablePlayeControl (){

		for (int i = 0; i < player.Length; i++) {
			player [i].EnableControls ();
		}
	}

	private void DisablePlayerControls (){

		for (int i = 0; i < player.Length; i++) {
			player [i].DisableControls ();
		}
	}*/
}