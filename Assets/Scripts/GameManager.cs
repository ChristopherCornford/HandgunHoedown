using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public UI_Manager UI_Manager;
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
		// UI_Manager = GameObject.Find("Managers/UI_Manager").GetComponent<UI_Manager>();
	}
	// DEBUG INPUTS
	 void Update(){
		if(Input.GetKeyDown(KeyCode.L)){StartCoroutine("RoundStart");}
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
		//TODO:  Write a corutine for counting down the start of the round, displaying that as UI
		int countDown = 3;
		yield return new WaitForSeconds(1.0f);
			countDown -= 1;
		yield return new WaitForSeconds(1.0f);
			countDown -= 1;
		yield return new WaitForSeconds(1.0f);
			countDown -= 1;
		yield return null;
	}

	public void RoundEnd(int playerindex){
		// TODO: Currently happens instantaneously - need to wait fo seconds, run coroutine
		SetPlayerInput(false);
		// TODO: Put the UI stuff in here too
		switch (playerindex){
			case 1:
				player1Score++;
				UI_Manager.GiveStars(playerindex, player1Score);
				break;
			case 2:
				player2Score++;
				UI_Manager.GiveStars(playerindex, player2Score);
				break;
		}
		Debug.Log(player1Score);
		Debug.Log(player2Score);
		if (player1Score == 3 || player2Score == 3){GameEnd(playerindex);}
		else {StartCoroutine("RoundStart");}
	}

	private void GameEnd(int playerindex){
		SetPlayerInput(false);
		// TODO: Show end of game message, play music cue
		// TODO: Display UI prompt to play again or change level or quit
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