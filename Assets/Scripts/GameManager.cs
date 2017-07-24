using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {
	public UI_Manager UI_Manager;
	public GunSpawn GunSpawn;
	
	[HeaderAttribute("Player Data")]
	public int player1Score;
	public int player2Score;
	public bool isPaused = false;
	
	[HeaderAttribute("Player References")]
	public PlayerManager[] player;
	public GameObject playerPrefab;
	public CameraControl cameraControl;
	public GameObject t_camera;

	/* private */
	
	void Awake () {
		SpawnAllPlayers ();
		StartCoroutine ("RoundStart");
		SetCameraTargets ();
		cameraControl.SetStartPositionAndSize ();
	}

	/*** PAUSING THE GAME ***/
	// TODO: PAUSE THE FUCKING GAME
	public void Pause(){
		Debug.Log("The game is now paused!");
		SetPlayerInput(false);
		UI_Manager.PauseGameMenu.SetActive(true);
		EventSystem.current.SetSelectedGameObject(UI_Manager.PlayAgain);
		isPaused = true;
	}
	public void Resume(){
		UI_Manager.PauseGameMenu.SetActive(true);
		isPaused = false;
		SetPlayerInput(true);
	}

	/*** ROUND LOGIC ***/
	private IEnumerator RoundStart(){
		SetPlayerInput(false);
		for (int i = 0; i < player.Length; i++) {
			player[i].instance.transform.position = player[i].spawnPoint.position;
			player[i].instance.transform.rotation = player[i].spawnPoint.rotation;
			player[i].instance.GetComponent<PlayerMovement>().Reset();
		}
		// Passing 3 to this uses another case that runs a for loop and removes all bullets
		UI_Manager.removeBullets(3,0);
		// If we wanted to lerp the camera add that code here - yield return StarCoroutine
		yield return StartCoroutine(UI_Manager.RoundStartCountdown());
		SetPlayerInput(true);
		GunSpawn.Spawning();
		yield return null;
	}

	public IEnumerator RoundEnd(int playerindex){
		SetPlayerInput(false);
		switch (playerindex){
			case 1:
				player1Score++;
				UI_Manager.giveStars(playerindex, player1Score);
				yield return StartCoroutine(UI_Manager.Message(UI_Manager.Player1Win, 3f));
				break;
			case 2:
				player2Score++;
				UI_Manager.giveStars(playerindex, player2Score);
				yield return StartCoroutine(UI_Manager.Message(UI_Manager.Player2Win, 3f));
				break;
		}
		if (player1Score == 3 || player2Score == 3){StartCoroutine(GameEnd(playerindex));}
		else {StartCoroutine("RoundStart");}
		yield return null;
	}

	private IEnumerator GameEnd(int playerindex){
		SetPlayerInput(false);
		switch (playerindex){
			case 1:
				yield return StartCoroutine(UI_Manager.Message(UI_Manager.Player1Win, 3f));
				break;
			case 2:
				yield return StartCoroutine(UI_Manager.Message(UI_Manager.Player2Win, 3f));
				break;
		}
		// TODO: Change music cue
		UI_Manager.EndGameMenu.SetActive(true);
		EventSystem.current.SetSelectedGameObject(GameObject.Find("PlayAgain_B"));
		
	}

	/*** PLAYER RELATED SHIT ***/
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