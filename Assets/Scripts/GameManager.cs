using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	[HeaderAttribute("Player Data")]
	public int player1Score;
	public int player2Score;
	
	[HeaderAttribute("Player References")]
	public PlayerManager[] player;
	public GameObject playerPrefab;
	public CameraControl cameraControl;
	public GameObject t_camera;

	void Awake () {
		SpawnAllPlayers ();
		SetCameraTargets ();
		cameraControl.SetStartPositionAndSize ();
	}
	// DEBUG INPUTS
	 void Update(){
		if(Input.GetKeyDown(KeyCode.L)){StartCoroutine("RoundStart");}
	} 

	public void PlayerScore(int playerNumber){
		// there's defintiely a better way to do this but i'll come back to it
		if (playerNumber == 1){ player1Score++;}
		if (playerNumber == 2){ player2Score++;}
		if (player1Score == 3 || player2Score == 3){GameEnd(playerNumber);}
		else {RoundEnd();}
	}

	private IEnumerator RoundStart(){
		SetPlayerInput(false);
		for (int i = 0; i < player.Length; i++) {
			player[i].instance.transform.position = player[i].spawnPoint.position;
			player[i].instance.transform.rotation = player[i].spawnPoint.rotation;
		}
		yield return StartCoroutine("RoundStartCountdown");
		SetPlayerInput(true);
	}

	private IEnumerator RoundStartCountdown(){
		// Write a corutine for counting down the start of the round, displaying that as UI
		int countDown = 3;
		yield return new WaitForSeconds(1.0f);
			countDown -= 1;
		yield return new WaitForSeconds(1.0f);
			countDown -= 1;
		yield return new WaitForSeconds(1.0f);
			countDown -= 1;
		yield return null;
	}

	private void RoundEnd(){
		SetPlayerInput(false);
	}

	private void GameEnd(int playerNumber){
		// Disable player movement
		SetPlayerInput(false);
		// Show end of game message, play music cue
		// Display UI prompt to play again or change level or quit
	}

	// Player Related Shit
	private void SetPlayerInput(bool status){
		for (int i = 0; i < player.Length; i++){
			player[i].instance.GetComponent<PlayerMovement>().enabled = status;
		}
	}
	private void SpawnAllPlayers() {
		// Spawn players at spawn points
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
}