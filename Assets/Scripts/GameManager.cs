﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {
	public UI_Manager UI_Manager;
	public SoundManager SoundManager;
	public GunSpawn GunSpawn;
	
	[Header("Gun spawn delay")]
	public float Gun_Spawn_Wait = 5.0f;
	
	[Header("Time between rounds")]
	public float Round_End_Wait = 3.0f;
	
	[Header("Player Data")]
	public int player1Score;
	public int player2Score;
	public bool isPaused = false;
	
	[Header("Player References")]
	public PlayerManager[] player;
	public GameObject playerPrefab;
	public CameraControl cameraControl;
	public GameObject t_camera;

	/* private */
	[HideInInspector]
	public Coroutine currentGunSpawn;
	
	void Start () {
		Time.timeScale = 1.0f;
		SpawnAllPlayers ();
		SetCameraTargets ();
		cameraControl.SetStartPositionAndSize ();
		SetPlayerInput(false);
		EventSystem.current.SetSelectedGameObject(GameObject.Find("HowToPlay_Button"));
	}

	/*** PAUSING THE GAME ***/
	public void Pause(){
		Debug.Log("The game is now paused!");
		SetPlayerInput(false);
		Time.timeScale = 0.0f;
		UI_Manager.PauseGameMenu.SetActive(true);
		SoundManager.music_source.Pause();
		EventSystem.current.SetSelectedGameObject(GameObject.Find("Resume_B"));
		isPaused = true;
	}
	public void Resume(){
		UI_Manager.PauseGameMenu.SetActive(false);
		SoundManager.music_source.Play();
		isPaused = false;
		SetPlayerInput(true);
		Time.timeScale = 1.0f;
	}

	/*** ROUND LOGIC ***/
	// Used to start the game from the how-to-play screen
	public void RoundStartUtil(){
		GameObject.Find("PlayCanvas/How-to-Play").SetActive(false);
		StartCoroutine (RoundStart());
	}
	private IEnumerator RoundStart(){
		// Destroys guns already spawned on the field
		DestroyAllGuns();
		SetPlayerInput(false);
		for (int i = 0; i < player.Length; i++) {
			player[i].instance.transform.position = player[i].spawnPoint.position;
			player[i].instance.transform.rotation = player[i].spawnPoint.rotation;
			PlayerMovement PlayerMovement = player[i].instance.GetComponent<PlayerMovement>();
			PlayerMovement.Reset();
			PlayerMovement.sprintCD = 1.5f;
			PlayerMovement.SetSprintUI ();
		}
		/* Passing 3 to this uses another case that runs a for loop and 
		removes all bullets from both players */
		UI_Manager.removeBullets(3,0);
		yield return StartCoroutine(UI_Manager.RoundStartCountdown());
		if(player1Score == 2 | player2Score == 2){SoundManager.SetMusic(1);}
		else {SoundManager.SetMusic(0);}
		SetPlayerInput(true);
		GunSpawn.numGuns = 0;
		StartCoroutine(GunSpawn.SpawnGun(Gun_Spawn_Wait));
		yield return null;
	}

	public IEnumerator RoundEnd(int playerindex){
		StopCoroutine(currentGunSpawn);
		SetPlayerInput(false);	
		// For loop to disable line renderer, currently only does it on person who hits
		for (int i = 0; i < player.Length; i++){
			PlayerMovement current =  player[i].instance.GetComponent<PlayerMovement>();
			current.cowboy_anim.SetBool("isMoving", false);
			current.line.enabled = false;
		}
		SoundManager.SetMusic(2);
		switch (playerindex){
			case 1:
				player1Score++;
				UI_Manager.giveStars(playerindex, player1Score);
				break;
			case 2:
				player2Score++;
				UI_Manager.giveStars(playerindex, player2Score);
				break;
		}
		GunSpawn.numGuns = 0;
		yield return new WaitForSeconds(Round_End_Wait);
		if (player1Score == 3 || player2Score == 3){StartCoroutine(GameEnd(playerindex));}
		else {StartCoroutine("RoundStart");}
		yield return null;
	}

	private IEnumerator GameEnd(int playerindex){
		SoundManager.SetMusic(3);
		switch (playerindex){
			case 1:
				yield return StartCoroutine(UI_Manager.Message(UI_Manager.Player1Win, 3f));
				break;
			case 2:
				yield return StartCoroutine(UI_Manager.Message(UI_Manager.Player2Win, 3f));
				break;
		}
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
	private void DestroyAllGuns(){
		GameObject[] guns = GameObject.FindGameObjectsWithTag("Gun");
		for (int i = 0; i < guns.Length; i++){
			Destroy(guns[i]);
			GunSpawn.RemoveIndicator(guns[i].GetComponent<GunPickupDestroyer>().myTransform);
		}
	}
}